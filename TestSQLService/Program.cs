using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            //While debugging this section is used.
            TestSQLService testSQLService = new TestSQLService();
            testSQLService.onDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#else

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TestSQLService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
