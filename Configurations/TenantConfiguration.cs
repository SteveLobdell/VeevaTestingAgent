using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Configurations namespace.
/// </summary>
namespace ReachOutVeevaPromoMats.Configurations
{
    /// <summary>
    /// Class TenantConfiguration.
    /// </summary>
    public class TenantConfiguration
    {
        /// <summary>
        /// The logger
        /// </summary>
        public static readonly Logger Logger = NLog.LogManager.GetLogger(ConfigurationManager.AppSettings["standardlogger"].ToString());

        /// <summary>
        /// The drop file enabled
        /// </summary>
        public static readonly bool DropFileEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["dropfileenabled"]);

        /// <summary>
        /// The drop folder
        /// </summary>
        public static readonly string DropFolder = ConfigurationManager.AppSettings["dropfiledirectory"].ToString();

        /// <summary>
        /// The drop folder
        /// </summary>
        public static readonly int DropFilePollFrequency = Convert.ToInt32(ConfigurationManager.AppSettings["dropfilepollfrequency"]);

        /// <summary>
        /// The archive drop files
        /// </summary>
        public static readonly bool ArchiveDropFiles = Convert.ToBoolean(ConfigurationManager.AppSettings["dropfilearchiveenabled"]);

        /// <summary>
        /// The able URL
        /// </summary>
        public static readonly string AbleAuthUrl = ConfigurationManager.AppSettings["ableauthurl"].ToString();

        /// <summary>
        /// The able authentication user
        /// </summary>
        public static readonly string AbleAuthUser = ConfigurationManager.AppSettings["ableauthuser"].ToString();

        /// <summary>
        /// The able authentication secret
        /// </summary>
        public static readonly string AbleAuthSecret = ConfigurationManager.AppSettings["ableauthsecret"].ToString();

        /// <summary>
        /// The able authentication action
        /// </summary>
        public static readonly string AbleAuthAction = ConfigurationManager.AppSettings["ableauthaction"].ToString();

        /// <summary>
        /// The able post items action
        /// </summary>
        public static readonly string AblePostItemsAction = ConfigurationManager.AppSettings["ablepostitemsaction"].ToString();

        /// <summary>
        /// The able veeva action
        /// </summary>
        public static readonly string AbleVeevaAction = ConfigurationManager.AppSettings["ableveevaaction"].ToString();

        /// <summary>
        /// The reach out client identifier
        /// </summary>
        public static readonly string ReachOutClientID = ConfigurationManager.AppSettings["reachoutclientid"].ToString();

        /// <summary>
        /// The veeva API enabled
        /// </summary>
        public static readonly bool VeevaAPIEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["veevaapienabled"]);

        /// <summary>
        /// The veeva API endpoint
        /// </summary>
        private static readonly string VeevaUrl = ConfigurationManager.AppSettings["veevaurl"].ToString();

        /// <summary>
        /// The veeva API version
        /// </summary>
        private static readonly string VeevaAPIVersion = ConfigurationManager.AppSettings["veevaapiversion"].ToString();

        /// <summary>
        /// The veeva API user
        /// </summary>
        public static readonly string VeevaAPIUser = ConfigurationManager.AppSettings["veevauser"].ToString();

        /// <summary>
        /// The veeva API password
        /// </summary>
        public static readonly string VeevaAPIPassword = ConfigurationManager.AppSettings["veevapassword"].ToString();

        /// <summary>
        /// The veeva result page size
        /// </summary>
        public static readonly string VeevaResultPageSize = ConfigurationManager.AppSettings["veevaresultpagesize"].ToString();

        /// <summary>
        /// The veeva result page size
        /// </summary>
        public static readonly double VeevaSweepTimeOverlapHours = Convert.ToDouble(ConfigurationManager.AppSettings["veevasweeptimeoverlaphours"]);

        /// <summary>
        /// The debug
        /// </summary>
        public static readonly bool Debug = Convert.ToBoolean(ConfigurationManager.AppSettings["debug"]);

        /// <summary>
        /// The allow add product
        /// </summary>
        public static readonly bool AllowAddProduct = Convert.ToBoolean(ConfigurationManager.AppSettings["allowaddproduct"]);

        /// <summary>
        /// The run at startup
        /// </summary>
        public static readonly bool RunAtStartup = Convert.ToBoolean(ConfigurationManager.AppSettings["runatstartup"]);

        /// <summary>
        /// Gets the next sweep interval.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double GetNextSweepInterval()
        {
            // Defaults to Today, but could already have passed
            DateTimeOffset dateTime = Convert.ToDateTime(ConfigurationManager.AppSettings["veevaapisweeptime"]);
            while (dateTime < DateTimeOffset.Now)
            {
                dateTime = dateTime.AddDays(1);
            }

            return (dateTime - DateTimeOffset.Now).TotalMilliseconds;
        }

        /// <summary>
        /// Gets the veeva API endpoint.
        /// </summary>
        /// <returns>The endpoint with api version</returns>
        public static string GetVeevaAPIEndpoint()
        {
            return VeevaUrl + "/api/" + VeevaAPIVersion;
        }

        /// <summary>
        /// Gets the veeva API base address.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetVeevaAPIBaseAddress()
        {
            return VeevaUrl;
        }

        /// <summary>
        /// Gets the next page URL.
        /// </summary>
        /// <param name="nextPage">The next page.</param>
        /// <returns>The Url for the next page</returns>
        public static string GetNextPageUrl(string nextPage)
        {
            return VeevaUrl + nextPage;
        }
    }
}
