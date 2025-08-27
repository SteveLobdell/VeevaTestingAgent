using Microsoft.Extensions.Configuration;
using NLog;
using System;

namespace ReachOutVeevaPromoMats.Configurations
{
    /// <summary>
    /// Configuration settings for the application using modern .NET configuration
    /// </summary>
    public class AppConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly Logger _logger;

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetLogger(_configuration["ReachOutSettings:StandardLogger"] ?? "standardlogger");
        }

        public Logger Logger => _logger;

        // ReachOut Settings
        public string InstanceID => _configuration["ReachOutSettings:InstanceID"] ?? "";
        public string StandardLogger => _configuration["ReachOutSettings:StandardLogger"] ?? "standardlogger";
        public string AbleAuthUrl => _configuration["ReachOutSettings:AbleAuthUrl"] ?? "";
        public string AbleAuthUser => _configuration["ReachOutSettings:AbleAuthUser"] ?? "";
        public string AbleAuthSecret => _configuration["ReachOutSettings:AbleAuthSecret"] ?? "";
        public string AbleAuthAction => _configuration["ReachOutSettings:AbleAuthAction"] ?? "";
        public string AbleVeevaAction => _configuration["ReachOutSettings:AbleVeevaAction"] ?? "";
        public string AblePostItemsAction => _configuration["ReachOutSettings:AblePostItemsAction"] ?? "";
        public string ReachOutClientID => _configuration["ReachOutSettings:ReachOutClientId"] ?? "";

        // General Settings
        public bool Debug => _configuration.GetValue<bool>("General:Debug");
        public bool AllowAddProduct => _configuration.GetValue<bool>("General:AllowAddProduct");
        public bool RunAtStartup => _configuration.GetValue<bool>("General:RunAtStartup");

        // Veeva API Settings
        public bool VeevaAPIEnabled => _configuration.GetValue<bool>("VeevaAPI:Enabled");
        public string VeevaAPISweepTime => _configuration["VeevaAPI:SweepTime"] ?? "11:00PM";
        public int VeevaSweepTimeOverlapHours => _configuration.GetValue<int>("VeevaAPI:SweepTimeOverlapHours", 3);
        public string VeevaAPIVersion => _configuration["VeevaAPI:ApiVersion"] ?? "v23.1";
        public string VeevaUrl => _configuration["VeevaAPI:Url"] ?? "";
        public string VeevaUser => _configuration["VeevaAPI:User"] ?? "";
        public string VeevaPassword => _configuration["VeevaAPI:Password"] ?? "";
        public int VeevaResultPageSize => _configuration.GetValue<int>("VeevaAPI:ResultPageSize", 100);

        // Drop File Settings
        public bool DropFileEnabled => _configuration.GetValue<bool>("DropFile:Enabled");
        public int DropFilePollFrequency => _configuration.GetValue<int>("DropFile:PollFrequency", 1);
        public bool ArchiveDropFiles => _configuration.GetValue<bool>("DropFile:ArchiveEnabled");
        public string DropFolder => _configuration["DropFile:Directory"] ?? "";

        /// <summary>
        /// Gets the next sweep interval for API calls.
        /// </summary>
        /// <returns>Interval in milliseconds</returns>
        public double GetNextSweepInterval()
        {
            // Parse sweep time
            TimeSpan sweepTime;
            if (!TimeSpan.TryParse(VeevaAPISweepTime.Replace("PM", "").Replace("AM", ""), out sweepTime))
            {
                sweepTime = new TimeSpan(23, 0, 0); // Default to 11:00 PM
            }

            // Adjust for PM if needed
            if (VeevaAPISweepTime.Contains("PM") && sweepTime.Hours < 12)
            {
                sweepTime = sweepTime.Add(TimeSpan.FromHours(12));
            }

            DateTime today = DateTime.Today;
            DateTime sweepDateTime = today.Add(sweepTime);

            // If sweep time has passed today, schedule for tomorrow
            if (sweepDateTime <= DateTime.Now)
            {
                sweepDateTime = sweepDateTime.AddDays(1);
            }

            double intervalMs = (sweepDateTime - DateTime.Now).TotalMilliseconds;
            return intervalMs;
        }

        /// <summary>
        /// Gets the veeva API endpoint.
        /// </summary>
        /// <returns>The endpoint with api version</returns>
        public string GetVeevaAPIEndpoint()
        {
            return VeevaUrl + "/api/" + VeevaAPIVersion;
        }

        /// <summary>
        /// Gets the veeva API base address.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetVeevaAPIBaseAddress()
        {
            return VeevaUrl;
        }

        /// <summary>
        /// Gets the next page URL.
        /// </summary>
        /// <param name="nextPage">The next page.</param>
        /// <returns>The Url for the next page</returns>
        public string GetNextPageUrl(string nextPage)
        {
            return VeevaUrl + nextPage;
        }
    }
}