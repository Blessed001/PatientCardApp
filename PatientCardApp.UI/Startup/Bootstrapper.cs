using Autofac;
using PatientCardApp.DataAccess;
using PatientCardApp.UI.Data;
using PatientCardApp.UI.Data.Lookups;
using PatientCardApp.UI.Data.Repositories;
using PatientCardApp.UI.View.Services;
using PatientCardApp.UI.ViewModel;
using Prism.Events;

namespace PatientCardApp.UI.Startup
{
    public class Bootstrapper
    {        
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<PatientCardContext>().AsSelf();
            
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<PatientCardDetailViewModel>().As<IPatientCardDetailViewModel>();

            builder.RegisterType<LookUpDataService>().AsImplementedInterfaces();
            builder.RegisterType<PatientCardRepository>().As<IPatientCardRepository>();

            return builder.Build();
        }
    }
}
