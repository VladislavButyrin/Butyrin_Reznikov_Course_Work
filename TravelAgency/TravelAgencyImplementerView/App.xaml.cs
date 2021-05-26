using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplements.Implements;
using Unity;
using Unity.Lifetime;
using System.Windows;
using TravelAgencyBusinessLogic.HelperModels;
using System.Configuration;
using System;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyImplementerView
{
    public partial class App : Application
    {
        public static UserViewModel User { get; set; }
        private void App_Startup(object sender, StartupEventArgs e)
        {
            var container = BuildUnityContainer();
            var form = container.Resolve<FirstWindow>();
            SetMailConfig();
            form.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ITourStorage, TourStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExcursionStorage, ExcursionStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGroupStorage, GroupStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IUserStorage, UserStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGuideStorage, GuideStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPlaceStorage, PlaceStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<TourLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GuideLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GroupLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<UserLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogicImplementer>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PlaceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<StatisticsLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
        private void SetMailConfig()
        {
            MailLogicImplementer.MailConfig(new MailConfig
            {
                SmtpHost = ConfigurationManager.AppSettings["SmtpHost"],
                SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]),
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
            });
        }
    }
}