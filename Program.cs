using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ReachOutVeevaPromoMats
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if(Environment.UserInteractive)
            {
                BrokerService.doWork();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new BrokerService()
                };

                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
