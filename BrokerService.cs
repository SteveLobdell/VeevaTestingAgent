using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using NLog;
using ReachOutVeevaPromoMats.Configurations;

namespace ReachOutVeevaPromoMats
{
    /// <summary>
    ///   Service to update item status
    /// </summary>
    public partial class BrokerService : ServiceBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly Logger Logger = NLog.LogManager.GetLogger(ConfigurationManager.AppSettings["standardlogger"].ToString());

        /// <summary>
        /// The drop file timer
        /// </summary>
        private static Timer DropFileTimer = new Timer();

        /// <summary>
        /// The API timer
        /// </summary>
        private static Timer APITimer = new Timer();

        /// <summary>
        /// The API sweep time
        /// </summary>
        private DateTime APISweepTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokerService"/> class.
        /// </summary>
        public BrokerService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The first run
        /// </summary>
        private bool APISweepAll = true;

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                this.StartTimers();
                Logger.Info("Service Started.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Fatal error in OnStart: " + ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Starts the timers.
        /// </summary>
        public void StartTimers()
        {
            if (TenantConfiguration.DropFileEnabled)
            {
                Logger.Info("Starting drop file timer.");

                DropFileTimer.Elapsed += new ElapsedEventHandler(this.OnDropFileTimerEvent);
                DropFileTimer.Interval = TenantConfiguration.RunAtStartup ? 100 : TenantConfiguration.DropFilePollFrequency * 60000;
                DropFileTimer.Enabled = true;
            }

            if (TenantConfiguration.VeevaAPIEnabled)
            {
                Logger.Info("Starting api timer.");

                APITimer.Elapsed += new ElapsedEventHandler(this.OnAPITimerEvent);
                APITimer.Interval = TenantConfiguration.RunAtStartup ? 100 : this.GetNextSweepInterval();
                APITimer.Enabled = true; 
            }
        }

        /// <summary>
        /// Gets the next sweep interval.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double GetNextSweepInterval()
        {
            double interval = TenantConfiguration.GetNextSweepInterval();
            Logger.Info("API sweep scheduled for {0}",DateTime.Now.AddMilliseconds(interval));

            return interval;
        }

        /// <summary>Called when [timed event].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void OnDropFileTimerEvent(object sender, ElapsedEventArgs e)
        {
            DropFileTimer.Enabled = false;

            BusinessLogic.ProcessDroppedFiles();

            DropFileTimer.Interval = TenantConfiguration.DropFilePollFrequency * 60000;
            DropFileTimer.Enabled = true;
        }

        /// <summary>Called when [timed event].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void OnAPITimerEvent(object sender, ElapsedEventArgs e)
        {
            APITimer.Enabled = false;

            BusinessLogic.SweepAPIForProducts();

            APITimer.Interval = this.GetNextSweepInterval();
            this.APISweepAll = false;

            APITimer.Enabled = true;
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        public static void doWork()
        {
            if (TenantConfiguration.DropFileEnabled)
            {
                BusinessLogic.ProcessDroppedFiles();
            }

            if (TenantConfiguration.VeevaAPIEnabled)
            {
                BusinessLogic.SweepAPIForProducts();
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
        }
    }
}
