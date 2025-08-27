using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReachOutVeevaPromoMats.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ReachOutVeevaPromoMats
{
    /// <summary>
    /// Background service for processing Veeva promotional materials
    /// </summary>
    public class VeevaBackgroundService : BackgroundService
    {
        private readonly ILogger<VeevaBackgroundService> _logger;
        private readonly AppConfiguration _configuration;
        private System.Timers.Timer? _dropFileTimer;
        private System.Timers.Timer? _apiTimer;
        private bool _apiSweepAll = true;

        public VeevaBackgroundService(ILogger<VeevaBackgroundService> logger, AppConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Veeva Background Service starting...");
                StartTimers();
                _logger.LogInformation("Veeva Background Service started.");

                // Keep the service running until cancellation is requested
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when cancellation is requested
                _logger.LogInformation("Veeva Background Service stopping due to cancellation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error in Veeva Background Service: {Message}", ex.Message);
                throw;
            }
            finally
            {
                StopTimers();
                _logger.LogInformation("Veeva Background Service stopped.");
            }
        }

        /// <summary>
        /// Starts the timers for drop file and API processing.
        /// </summary>
        private void StartTimers()
        {
            if (_configuration.DropFileEnabled)
            {
                _logger.LogInformation("Starting drop file timer.");

                _dropFileTimer = new System.Timers.Timer();
                _dropFileTimer.Elapsed += OnDropFileTimerEvent;
                _dropFileTimer.Interval = _configuration.RunAtStartup ? 100 : _configuration.DropFilePollFrequency * 60000;
                _dropFileTimer.Enabled = true;
            }

            if (_configuration.VeevaAPIEnabled)
            {
                _logger.LogInformation("Starting API timer.");

                _apiTimer = new System.Timers.Timer();
                _apiTimer.Elapsed += OnAPITimerEvent;
                _apiTimer.Interval = _configuration.RunAtStartup ? 100 : GetNextSweepInterval();
                _apiTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Stops the timers.
        /// </summary>
        private void StopTimers()
        {
            _dropFileTimer?.Stop();
            _dropFileTimer?.Dispose();
            _dropFileTimer = null;

            _apiTimer?.Stop();
            _apiTimer?.Dispose();
            _apiTimer = null;
        }

        /// <summary>
        /// Gets the next sweep interval.
        /// </summary>
        /// <returns>Interval in milliseconds</returns>
        private double GetNextSweepInterval()
        {
            double interval = _configuration.GetNextSweepInterval();
            _logger.LogInformation("API sweep scheduled for {ScheduledTime}", DateTime.Now.AddMilliseconds(interval));
            return interval;
        }

        /// <summary>
        /// Handles the drop file timer event.
        /// </summary>
        private void OnDropFileTimerEvent(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_dropFileTimer != null)
                {
                    _dropFileTimer.Enabled = false;

                    var businessLogic = new BusinessLogic(_configuration);
                    businessLogic.ProcessDroppedFiles();

                    _dropFileTimer.Interval = _configuration.DropFilePollFrequency * 60000;
                    _dropFileTimer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in drop file timer event: {Message}", ex.Message);
            }
        }

        /// <summary>
        /// Handles the API timer event.
        /// </summary>
        private void OnAPITimerEvent(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_apiTimer != null)
                {
                    _apiTimer.Enabled = false;

                    var businessLogic = new BusinessLogic(_configuration);
                    businessLogic.SweepAPIForProducts();

                    _apiTimer.Interval = GetNextSweepInterval();
                    _apiSweepAll = false;

                    _apiTimer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in API timer event: {Message}", ex.Message);
            }
        }

        /// <summary>
        /// Executes work for interactive mode.
        /// </summary>
        public void DoWork()
        {
            var businessLogic = new BusinessLogic(_configuration);

            if (_configuration.DropFileEnabled)
            {
                businessLogic.ProcessDroppedFiles();
            }

            if (_configuration.VeevaAPIEnabled)
            {
                businessLogic.SweepAPIForProducts();
            }
        }

        public override void Dispose()
        {
            StopTimers();
            base.Dispose();
        }
    }
}