using Autofac;
using PatientCardApp.DataAccess;
using PatientCardApp.UI.Data;
using PatientCardApp.UI.ViewModel;

namespace PatientCardApp.UI.Startup
{
    public class Bootstrapper
    {        
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PatientCardContext>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<PatientCardDataServices>().As<IPatientCardDataServices>();

            return builder.Build();
        }
    }
}
