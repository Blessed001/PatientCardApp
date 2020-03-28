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
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<PatientCardDatailViewModel>().As<IPatientCardDatailViewModel>();

            builder.RegisterType<LookUpDataService>().AsImplementedInterfaces();
            builder.RegisterType<PatientCardDataServices>().As<IPatientCardDataService>();

            return builder.Build();
        }
    }
}
