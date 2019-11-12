using GivingBackChange.Business.IoC;
using GivingBackChange.Dal.IoC;
using Unity;

namespace GivingBackChange.Ui.IoC
{
    public static class UnityConfigurationBase
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container => _container = _container ?? new UnityContainer();

        public static void RegisterDependencies()
        {
            UnityConfigurationBusiness.RegisterDependencies(Container);
            UnityConfigurationDal.RegisterDependencies(Container);
        }
    }
}
