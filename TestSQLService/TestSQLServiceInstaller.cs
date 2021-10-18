using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace TestSQLService
{
    [RunInstaller(true)]
    public partial class TestSQLServiceInstaller : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public TestSQLServiceInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "TestSQLService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}