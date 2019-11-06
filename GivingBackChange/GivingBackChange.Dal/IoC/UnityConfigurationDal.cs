using GivingBackChange.Dal.Managers;
using GivingBackChange.Dal.Managers.Implementations.Fakes;
using Unity;
using Unity.Lifetime;

namespace GivingBackChange.Dal.IoC
{
    public static class UnityConfigurationDal
    {
        public static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IStoreManager, FakeStoreManager>(new ContainerControlledLifetimeManager());
        }
    }
}
