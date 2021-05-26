using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyEntitiesImplements.Implements;
using Unity;
using Unity.Lifetime;
using System.Windows;

namespace TravelAgencyView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int OrganizatorId;
        private void App_Startup(object sender, StartupEventArgs e)
        {
            var container = BuildUnityContainer();
            var form = container.Resolve<RegisterLoginFrame>();
            form.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ITripStorage, TripStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGuideStorage, GuideStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrganizatorStorage, OrganizatorStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPlaceStorage, PlaceStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGroupStorage, GroupStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExcursionStorage, ExcursionStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoStorage, MessageInfoStorage>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<TripBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GuideBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PlaceBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrganizatorBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GroupBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
