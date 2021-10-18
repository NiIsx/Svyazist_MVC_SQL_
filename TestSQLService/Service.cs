using System;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;

namespace TestSQLService
{
    public partial class TestSQLService : ServiceBase
    {
        Loggers.Folder loggersFolder;
        Loggers.Sql loggerSql;

        public TestSQLService()
        {
            InitializeComponent();

            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }
        protected override void OnStart(string[] args)
        {
            loggersFolder = new Loggers.Folder();
            Thread loggerThread = new Thread(new ThreadStart(loggersFolder.Start));
            loggerThread.Start();

            loggerSql = new Loggers.Sql();
            Thread.Sleep(1000);
            loggerSql.RefreshData();
        }

        protected override void OnStop()
        {
            //loggerSql
            loggersFolder.Stop();
            Thread.Sleep(1000);
        }

        public void onDebug()
        {
            OnStart(null);
        }
    }
}
