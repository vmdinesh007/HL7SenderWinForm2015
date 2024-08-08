using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Sockets;
using NHapi.Base.Parser;
using NHapi.Base.Model;
using log4net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;

namespace HL7SenderWinForm2015
{
    public partial class Form1 : Form
    {
        private static readonly ILog AppLog = log4net.LogManager.GetLogger("RollingFileAppender");


        X509Certificate clientCertificate = null;
        public Form1()
        {
            InitializeComponent();
        }

        public bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                AppLog.Info("No Certificate Errors.");
                return true;
            }
            else
            {
                AppLog.Error("Certificate error: "+ sslPolicyErrors);

                // Do not allow this client to communicate with unauthenticated servers.
                return false;
            }   

            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Setting the LOG Path
            if (ConfigurationManager.AppSettings["CustomPath"] == "True" && ConfigurationManager.AppSettings["CustomLogPath"] != "")
            {
                log4net.GlobalContext.Properties["LogFileName"] = ConfigurationManager.AppSettings["CustomLogPath"];
            }
            else
            {
                log4net.GlobalContext.Properties["LogFileName"] = Environment.CurrentDirectory + "\\Log\\log"; //log file path
            }
            log4net.Config.XmlConfigurator.Configure();

            try
            {
                //AppLog.Debug("\n\n");
                AppLog.Info("Preparing to send Message");
                string msHL7 = Regex.Replace(txtHL7.Text, @"\r\n|\n\r|\n|\r", "\r");
                //string msHL7 = txtHL7.Text.Replace("\n", "\r");

                AppLog.Info("Start parsing the Message");
                PipeParser pp = new PipeParser();
                IMessage msg = pp.Parse(Parse.removeNKwDTM(msHL7));
                AppLog.Info("End parsing the Message");
                string messageType = msg.GetStructureName();
                //string msg = "Not Null";
                bool sslcondition = bool.Parse(ConfigurationManager.AppSettings["SSL"]);// "tcp://localhost";
                if (msg != null)
                {
                    if (sslcondition)
                    {
                        RunClient(msHL7);
                    }
                    else
                    {
                        RunClientwithoutSSL(msHL7);
                    }
                }
                else
                {
                    AppLog.Error("Unable to parse message");
                    MessageBox.Show("Unable to parse the message");
                }
            }
            catch (Exception ex)
            {
                AppLog.Error("Exception : "+ ex);
                MessageBox.Show("Error : Unable to parse the message " + ex);
            }
        }

        public void RunClient(string HL7Message)
        {
            try
            {
                //Socket Sender 
                string srcAddress = ConfigurationManager.AppSettings["SRC"];// "tcp://localhost";
                int index = int.Parse(ConfigurationManager.AppSettings["IPAddressIndex"]);
                Uri src = new Uri(srcAddress);
                int Port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

                IPAddress[] ipAddresses = Dns.GetHostAddresses(src.Host);
                IPAddress ipAddr = ipAddresses[index];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Port);//Need to move to config file
                Socket sender = new Socket(ipAddr.AddressFamily,
                       SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(localEndPoint);

                AppLog.Info("Client connected.");

                NetworkStream networkStream = new NetworkStream(sender, true);

                SslStream sslStream = new SslStream(
                    networkStream,
                    false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate),
                    null
                    );

                sslStream.ReadTimeout = 20000;
                sslStream.WriteTimeout = 20000;

                string host = ConfigurationManager.AppSettings["HostName"];// "G07SGXNFAP11083"; //Need to move to config file

                bool twoWaySSL = bool.Parse(ConfigurationManager.AppSettings["2WaySSL"]);

                if (twoWaySSL)
                {
                    clientCertificate = Load(out X509Certificate2 x509Certificate2);
                    if (x509Certificate2 != null)
                    {
                        AppLog.Info("SSL Certificate loaded successfully ; Thumbprint : " + x509Certificate2.Thumbprint);

                    }
                    else
                    {
                        AppLog.Info("No SSL cert Loaded");
                    }
                    X509CertificateCollection CertificatesCollection = new X509CertificateCollection();
                    CertificatesCollection.Add(clientCertificate);

                    try
                    {
                        sslStream.AuthenticateAsClient(targetHost: host, clientCertificates: CertificatesCollection, enabledSslProtocols: SslProtocols.Tls12, checkCertificateRevocation: false);
                    }
                    catch (AuthenticationException e)
                    {
                        AppLog.Error("AuthenticationException " + e);
                        MessageBox.Show("AuthenticationException " + e);
                    }
                }
                else
                {
                    try
                    {
                        sslStream.AuthenticateAsClient(host);
                    }
                    catch (AuthenticationException e)
                    {
                        AppLog.Error("AuthenticationException " + e);
                        MessageBox.Show("AuthenticationException " + e);
                    }
                }

                StringBuilder sb = new StringBuilder("");//Extra charecter to be removed in PIB IE
                sb.Append(HL7Message);
                byte[] messsage = Encoding.UTF8.GetBytes(sb.ToString());
                //byte[] messsage = Encoding.UTF8.GetBytes("~"+HL7Message);
                

                sslStream.Write(messsage);
                sslStream.Flush();

                // Read message from the server.
                string serverMessage = ReadMessage(sslStream);
                AppLog.Info("ACK message from server :" + (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : "No ACK"));
                MessageBox.Show("ACK message from server : " + (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : "No ACK"));
            }
            catch (AuthenticationException e)
            {
                AppLog.Info("AuthenticationException " + e);
                MessageBox.Show("AuthenticationException " + e);
            }
            catch (IOException e)
            {
                MessageBox.Show("IOException " + e);
                AppLog.Info("IOException " + e);

            }
            catch(SocketException ex)
            {
                AppLog.Info("Socket Exception {ex} " , ex);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception " + e);
                AppLog.Info("Exception " + e);

            }
            finally
            {
                AppLog.Info("End sending message \n\n ");
            }

        }

        private void RunClientwithoutSSL(string HL7Message)
        {
            try
            {
                //Need to move to config file


                //Socket Sender 
                string srcAddress = ConfigurationManager.AppSettings["SRC"];// "tcp://localhost";
                int index = int.Parse(ConfigurationManager.AppSettings["IPAddressIndex"]);
                Uri src = new Uri(srcAddress);
                int Port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

                IPAddress[] ipAddresses = Dns.GetHostAddresses(src.Host);
                IPAddress ipAddr = ipAddresses[index];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Port);//Need to move to config file
                Socket sender = new Socket(ipAddr.AddressFamily,
                       SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(localEndPoint);

                AppLog.Info("Client connected.");

                NetworkStream networkStream = new NetworkStream(sender, true);

                StringBuilder sb = new StringBuilder("");//Extra charecter to be removed in PIB IE
                sb.Append(HL7Message);
                byte[] message = Encoding.UTF8.GetBytes("~" + HL7Message);

                networkStream.Write(message, 0, message.Length);
                networkStream.Flush();


                ////Socket Sender 
                //string srcAddress = ConfigurationManager.AppSettings["SRC"];// "tcp://localhost";
                //int index = int.Parse(ConfigurationManager.AppSettings["IPAddressIndex"]);
                //Uri src = new Uri(srcAddress);
                //int Port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

                //IPAddress[] ipAddresses = Dns.GetHostAddresses(src.Host);
                //IPAddress ipAddr = ipAddresses[index];

                //StringBuilder sb = new StringBuilder("");//Extra charecter to be removed in PIB IE
                //sb.Append(HL7Message);
                ////byte[] messsage = Encoding.UTF8.GetBytes(sb.ToString());
                //byte[] message = Encoding.UTF8.GetBytes("~" + HL7Message);

                //TcpListener tcpListener = new TcpListener(ipAddr, Port);
                //tcpListener.Start();

                //TcpClient tcpClient = tcpListener.AcceptTcpClient();
                //Stream stream = tcpClient.GetStream();


                //stream.Write(message,0,message.Length);
                //stream.Flush();

                //sslStream.Write(messsage);
                //sslStream.Flush();

                // Read message from the server.
                var buffer = new byte[1_024];

                //int received = stream.Read(buffer, 0, buffer.Length);
                int received = networkStream.Read(buffer, 0, buffer.Length);
                var serverMessage = Encoding.UTF8.GetString(buffer, 0, received);
                /*string serverMessage = ReadMessage(sslStream)*/
                ;
                AppLog.Info("ACK message from server :" + (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : "No ACK"));
                MessageBox.Show("ACK message from server : " + (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : "No ACK"));
            }
           
            catch (Exception e)
            {
                MessageBox.Show("Exception " + e);
                AppLog.Info("Exception " + e);

            }
            finally
            {
                AppLog.Info("End sending message \n\n ");
            }

        } 
        static string ReadMessage(SslStream sslStream)
        {
            byte[] buffer = new byte[8192];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                break;
            } while (bytes != 0);

            return messageData.ToString();
        }
        public X509Certificate2 Load(out X509Certificate2 x509_2)
        {
            AppLog.Info("Start Loading the SSL Certificate");

            x509_2 = null;
            X509Certificate2 x509Certificate = null;
            
            X509Store store;
            string certDir = System.Configuration.ConfigurationManager.AppSettings["CertDir"];
            if (certDir == "Personal")
            {
                store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            }
            else if (certDir == "Publisher")
            {
                store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
            }
            else if (certDir == "Root")
            {
                store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            }
            else
            {
                store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
            }

            string thumbPrint = StripTheSpacesAndMakeItUpper(ConfigurationManager.AppSettings["pfxthumbPrint"]);
            store.Open(OpenFlags.ReadOnly);
            var certCollection = store.Certificates;
            IEnumerable<X509Certificate2> certificate2s = store.Certificates.OfType<X509Certificate2>().Where(x => x.Thumbprint == thumbPrint);
            foreach (var x509 in certCollection)
            {
                if (x509.Thumbprint.Equals(thumbPrint))
                {
                    x509Certificate = x509;
                    x509_2 = x509;
                    break;
                }
            }

            store.Close();
            return x509Certificate;
        }
        private string StripTheSpacesAndMakeItUpper(string thumbPrint)
        {
            if (!string.IsNullOrWhiteSpace(thumbPrint))
            {
                return Regex.Replace(thumbPrint, @"\s|\W", "").ToUpper();
            }
            return thumbPrint;
        }


    }
}
