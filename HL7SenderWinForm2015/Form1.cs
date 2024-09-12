﻿using System;
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
        
        private static readonly ILog _log = log4net.LogManager.GetLogger("RollingFileAppender");
        private static string VT = "\v";
        private NetworkStream networkStream;
        private const string NoACK = "No ACK";

        X509Certificate2 clientCertificate = null;
        public Form1()
        {
            InitializeComponent();
        }

        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                _log.Info("No Certificate Errors.");
                return true;
            }
            else
            {
                _log.ErrorFormat("Certificate error: {0}", sslPolicyErrors);

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
                string msHL7 = Regex.Replace(txtHL7.Text, @"\r\n|\n\r|\n|\r", "\r");

                _log.Info("Start parsing the Message");
                PipeParser pp = new PipeParser();
                _log.Info("End parsing the Message");

                bool sslcondition = bool.Parse(ConfigurationManager.AppSettings["SSL"]);

                if (sslcondition)
                {
                    RunClient(msHL7);
                }
                else
                {
                    RunClientwithoutSSL(msHL7);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Exception : {ex}", ex);
                MessageBox.Show("Error : Unable to parse the message " + ex);
            }
        }

        private void configureSocketSender()
        {
            try
            {
                //Socket Sender 
                string srcAddress = ConfigurationManager.AppSettings["SRC"];
                int index = int.Parse(ConfigurationManager.AppSettings["IPAddressIndex"]);
                Uri src = new Uri(srcAddress);
                int Port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

                IPAddress[] ipAddresses = Dns.GetHostAddresses(src.Host);
                IPAddress ipAddr = ipAddresses[index];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Port);//Need to move to config file
                Socket sender = new Socket(ipAddr.AddressFamily,
                       SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(localEndPoint);

                _log.Info("Client connected.");

                networkStream = new NetworkStream(sender, true);

            }
            catch(Exception ex) 
            { 
                _log.Error("Set Socket Exception : {ex}",ex);
            }
        }
        public void RunClient(string HL7Message)
        {
            try
            {
                configureSocketSender();
                
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
                    clientCertificate = Load();
                    if (clientCertificate != null)
                    {
                        _log.InfoFormat("SSL Certificate loaded successfully ; Thumbprint : {TP}", clientCertificate.Thumbprint);

                    }
                    else
                    {
                        _log.Info("No SSL cert Loaded");
                        throw new ArgumentNullException("No SSL cert with the specified thumbprint found");
                    }
                    X509CertificateCollection CertificatesCollection = new X509CertificateCollection();
                    CertificatesCollection.Add(clientCertificate);

                    try
                    {
                        sslStream.AuthenticateAsClient(targetHost: host, clientCertificates: CertificatesCollection, enabledSslProtocols: SslProtocols.Tls12, checkCertificateRevocation: false);
                    }
                    catch (AuthenticationException e)
                    {
                        _log.Error("AuthenticationException {ex}", e);
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
                        _log.Error("AuthenticationException {ex}", e);
                        MessageBox.Show("AuthenticationException " + e);
                    }
                }

                StringBuilder sb = new StringBuilder(VT);//Extra charecter to be removed in PIB IE

                sb.Append(HL7Message);
                byte[] messsage = Encoding.UTF8.GetBytes(sb.ToString());
                

                sslStream.Write(messsage);
                sslStream.Flush();

                // Read message from the server.
                string serverMessage = ReadMessage(sslStream);
                _log.InfoFormat("ACK message from server : {ServerMessage} " , (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : NoACK));
                MessageBox.Show("ACK message from server : " + (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : NoACK));
            }
            catch (AuthenticationException e)
            {
                _log.Info("AuthenticationException {ex}", e);
                MessageBox.Show("AuthenticationException " + e);
            }
            catch (IOException e)
            {
                MessageBox.Show("IOException " + e);
                _log.Info("IOException {ex}", e);

            }
            catch(SocketException ex)
            {
                _log.Info("Socket Exception {ex} " , ex);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception " + e);
                _log.Info("Exception {ex}", e);

            }
            finally
            {
                _log.Info("End sending message \n\n ");
            }

        }

        private void RunClientwithoutSSL(string HL7Message)
        {
            try
            {
                //Need to move to config file

                configureSocketSender();
                
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

                int received = networkStream.Read(buffer, 0, buffer.Length);
                var serverMessage = Encoding.UTF8.GetString(buffer, 0, received);
                /*string serverMessage = ReadMessage(sslStream)*/
                
                _log.InfoFormat("ACK message from server :{0}" , (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : NoACK));
                MessageBox.Show("ACK message from server : " + (!string.IsNullOrWhiteSpace(serverMessage) ? serverMessage.Remove(0, 1) : NoACK));
            }
           
            catch (Exception e)
            {
                MessageBox.Show("Exception " + e);
                _log.Info("Exception {ex}", e);

            }
            finally
            {
                _log.Info("End sending message \n\n ");
            }

        } 
        static string ReadMessage(SslStream sslStream)
        {
            byte[] buffer = new byte[8192];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;

            bytes = sslStream.Read(buffer, 0, buffer.Length);

            Decoder decoder = Encoding.UTF8.GetDecoder();
            char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
            decoder.GetChars(buffer, 0, bytes, chars, 0);
            messageData.Append(chars);

            return messageData.ToString();
        }
        public X509Certificate2 Load()
        {
            _log.Info("Start Loading the SSL Certificate");

            X509Certificate2 x509Certificate = null;
            
            X509Store store = null;
            string certDir = System.Configuration.ConfigurationManager.AppSettings["CertDir"];

            if (!string.IsNullOrWhiteSpace(certDir))
            {
                switch (certDir)
                {
                    case "Personal":
                        store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                        break;

                    case "Publisher":
                        store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
                        break ;

                    case "Root":
                        store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                        break;

                    default:
                        store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
                        break;

                }
            }

            else
            {
                throw new ArgumentNullException("Store Name is not specified");
            }

            string thumbPrint = StripTheSpacesAndMakeItUpper(ConfigurationManager.AppSettings["pfxthumbPrint"]);
            store.Open(OpenFlags.ReadOnly);
            
            x509Certificate = store.Certificates.OfType<X509Certificate2>().Where(x => x.Thumbprint == thumbPrint).FirstOrDefault();
            
            store.Close();
            return x509Certificate;
        }
        private static string StripTheSpacesAndMakeItUpper(string thumbPrint)
        {
            if (!string.IsNullOrWhiteSpace(thumbPrint))
            {
                return Regex.Replace(thumbPrint, @"\s|\W", "").ToUpper();
            }
            return thumbPrint;
        }


    }
}
