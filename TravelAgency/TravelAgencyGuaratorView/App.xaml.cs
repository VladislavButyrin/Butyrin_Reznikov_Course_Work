using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplements.Implements;
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
            currentContainer.RegisterType<TripLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GuideLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PlaceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrganizatorLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogicImplementer>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GroupLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MailLogicImplementer>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
