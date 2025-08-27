using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using ReachOutVeevaPromoMats.Configurations;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReachOutVeevaPromoMats
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static async Task Main(string[] args)
        {
            try
            {
                // Build configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();

                // Configure NLog from configuration
                LogManager.Setup().LoadConfigurationFromFile("NLog.config");

                // Check if running interactively or as a service
                if (Environment.UserInteractive)
                {
                    // Running interactively - run once and exit
                    Console.WriteLine("Running in interactive mode...");
                    
                    var appConfig = new AppConfiguration(configuration);
                    var service = new VeevaBackgroundService(
                        new ConsoleLogger<VeevaBackgroundService>(), 
                        appConfig);
                    
                    service.DoWork();
                    Console.WriteLine("Work completed. Press any key to exit.");
                    Console.ReadKey();
                }
                else
                {
                    // Running as a Windows Service
                    var host = CreateHostBuilder(args, configuration).Build();
                    await host.RunAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex}");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Creates the host builder for the Windows Service.
        /// </summary>
        static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "ReachOutVeevaPromoMats";
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(configuration);
                    services.AddSingleton<AppConfiguration>();
                    services.AddHostedService<VeevaBackgroundService>();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddNLog();
                })
                .UseConsoleLifetime();
        }
    }

    /// <summary>
    /// Simple console logger for interactive mode
    /// </summary>
    public class ConsoleLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => null!;
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => true;

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Console.WriteLine($"[{logLevel}] {formatter(state, exception)}");
            if (exception != null)
            {
                Console.WriteLine(exception.ToString());
            }
        }
    }
}
