using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

/// <summary>
/// The ReachOutVeevaPromoMats namespace.
/// </summary>
namespace ReachOutVeevaPromoMats
{
    /// <summary>
    /// Class ProjectInstaller.
    /// Implements the <see cref="Installer" />
    /// </summary>
    /// <seealso cref="Installer" />
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the AfterInstall event of the serviceInstaller1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="InstallEventArgs"/> instance containing the event data.</param>
        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        /// <summary>
        /// Handles the AfterInstall event of the serviceProcessInstaller1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="InstallEventArgs"/> instance containing the event data.</param>
        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        /// <summary>
        /// Gets the display name of the service and.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="description">The description.</param>
        public void GetServiceNameAndDescription(out string serviceName, out string description)
        {
            string configurationFilePath = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "exe.config");
            XmlDocument doc = new XmlDocument();
            doc.Load(configurationFilePath);

            XmlNode instanceNode = doc.SelectSingleNode("//appSettings//add[@key='instanceID']");
            if (instanceNode != null && (instanceNode.Attributes != null && (instanceNode.Attributes["value"] != null)))
            {
                serviceName = "ReachOut Veeva ProMats " + instanceNode.Attributes["value"].Value;
            }
            else
            {
                serviceName = "ReachOut Veeva ProMats";
            }

            description = "Imports Veeva ProMats data for " + instanceNode.Attributes["value"].Value;
        }
    }
}
