using System;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;

namespace TestSQLService.Loggers
{
    internal class Sql
    {
       // internal string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Svyazist_MVC_SQL;Trusted_Connection=True;";
        object obj = new object();
        public void RefreshData()
        {
            try
            {
                // To prevent errors, when database connection is lost, always stop SqlDependency subscription, before starting one.
                SqlDependency.Stop(ConfigurationManager.ConnectionStrings["Svyazist_MVC_SQL"].ConnectionString);
                SqlDependency.Start(ConfigurationManager.ConnectionStrings["Svyazist_MVC_SQL"].ConnectionString);

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Svyazist_MVC_SQL"].ConnectionString))
                {
                    // Define query.
                    //string storedProcedureName = "Reporting.SqlDependency_GetProcesses";

                    using (SqlCommand command = new SqlCommand(ConfigurationManager.ConnectionStrings["Svyazist_MVC_SQL"].ConnectionString))//storedProcedureName, 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Make sure the command object does not already have a notification object associated with it.
                        command.Notification = null;

                        // Hookup sqldependency eventlistener (re-register for change events).
                        var dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                        // Open connection to database.
                        connection.Open();

                        // Get the data from the database and convert to a list of DTO's.
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.Error("Error.", ex);

                // Exception can be caused by a database connection los, so try to hookup sqldependency eventlistener again.
                RefreshData();
            }
        }

        /// <summary>
        /// Fires, when the data in the database changes.
        /// </summary>
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                lock (obj)
                {
                    using (StreamWriter writer = new StreamWriter("D:\\templogSql.txt", true))
                    {
                        writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "     " + e.Info);
                        writer.Flush();
                    }
                }
                //RefreshData();
            }
        }

        void Termination()
        {
            // Release the dependency.
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["Svyazist_MVC_SQL"].ConnectionString);
        }
    }
}
