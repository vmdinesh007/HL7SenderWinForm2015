using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HL7SenderWinForm2015
{
    [RunInstaller(true)]
    public partial class InstallerSetup : System.Configuration.Install.Installer
    {
        public InstallerSetup()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            try
            {
                AddConfigurationFileDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
                base.Rollback(savedState);
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
        public void AddConfigurationFileDetails()
        {
            try
            {
                string HostName = Context.Parameters["HostName"];
                string ServerPort = Context.Parameters["ServerPort"];
                string pfxthumbPrint = Context.Parameters["pfxthumbPrint"];
                string IPAddressIndex = Context.Parameters["IPAddressIndex"];

                // Get the path to the executable file that is being installed on the target computer  
                string assemblypath = Context.Parameters["assemblypath"];
                string appConfigPath = assemblypath + ".config";

                //Connection String
                StringBuilder sbDataSource = new StringBuilder();
                sbDataSource.Append("Integrated Security=false; ");
                sbDataSource.Append("Data Source = " + Context.Parameters["DataSource"] + ";");
                sbDataSource.Append("Database = " + Context.Parameters["Database"] + ";");
                sbDataSource.Append("User ID = " + Context.Parameters["UserID"] + ";");
                sbDataSource.Append("Password = " + Context.Parameters["Password"] + ";");

                             

                ExeConfigurationFileMap map = new ExeConfigurationFileMap();

                //Getting the path location 
                string configFile = string.Concat(Assembly.GetExecutingAssembly().Location, ".config");
                map.ExeConfigFilename = configFile;

                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(map, System.Configuration.ConfigurationUserLevel.None);
                string connectionsection = config.ConnectionStrings.ConnectionStrings["connectionString"].ConnectionString;


                ConnectionStringSettings connectionString = null;
                if (connectionsection != null)
                {
                    config.ConnectionStrings.ConnectionStrings.Remove("connectionString");
                }
                connectionString = new ConnectionStringSettings("connectionstring", sbDataSource.ToString());
                config.ConnectionStrings.ConnectionStrings.Add(connectionString);

                config.Save(ConfigurationSaveMode.Modified, false);
                ConfigurationManager.RefreshSection("connectionStrings");

                // Write the path to the app.config file  
                XmlDocument doc = new XmlDocument();
                doc.Load(appConfigPath);

                XmlNode configuration = null;

                foreach(XmlNode node in doc.ChildNodes)
                    if(node.Name == "configuration")
                        configuration = node;

                if(configuration != null)
                {
                    // Get the ‘appSettings’ node 
                    XmlNode settingNode = null;
                    foreach(XmlNode node in configuration.ChildNodes)
                    {
                        if (node.Name == "appSettings")
                            settingNode = node;
                    }
                    if(settingNode != null)
                    {
                        //Reassign values in the config file
                        foreach(XmlNode node in settingNode.ChildNodes)
                        {
                            //MessageBox.Show("node.Value = " + node.Value);
                            if(node.Attributes == null)
                                continue;
                            XmlAttribute attribute = node.Attributes["value"];

                            //MessageBox.Show("attribute != null ");  
                            //MessageBox.Show("node.Attributes['value'] = " + node.Attributes["value"].Value);

                            XmlAttribute xmlAttribute = node.Attributes["value"];
                            //MessageBox.Show("attribute != null ");  
                            //MessageBox.Show("node.Attributes['value'] = " + node.Attributes["value"].Value);  

                            if(node.Attributes["key"] != null)
                            {
                                switch (node.Attributes["key"].Value)
                                {
                                    case "HostName":
                                        attribute.Value = HostName;
                                        break;

                                    case "ServerPort":
                                        attribute.Value = ServerPort;
                                        break;

                                    case "pfxthumbPrint":
                                        attribute.Value = pfxthumbPrint;
                                        break;

                                    //case "SRC":
                                        //attribute.Value = SRC;
                                        //break;

                                    case "IPAddressIndex":
                                        attribute.Value = IPAddressIndex;
                                        break;
                                }
                            }
                        }
                    }
                    doc.Save(appConfigPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
            }
        }
    }
}
