using GivingBackChange.Business.Services;
using GivingBackChange.Business.Services.Implementations;
using GivingBackChange.Business.Services.Implementations.GetChangeServices;
using Unity;
using Unity.Lifetime;

namespace GivingBackChange.Business.IoC
{
    public static class UnityConfigurationBusiness
    {
        public static void RegisterDependencies(IUnityContainer container)
        {
#if DEBUGGREEDYRECALGORITHM
            container.RegisterType<IGetChangeService, GetChangeGreedyRecAlgorithmService>(new ContainerControlledLifetimeManager());
#elif DEBUGDYNAMICPROGRAMMING
            container.RegisterType<IGetChangeService, GetChangeDynamicProgrammingService>(new ContainerControlledLifetimeManager());
#else
            container.RegisterType<IGetChangeService, GetChangeGreedyAlgorithmService>(new ContainerControlledLifetimeManager());
#endif
            container.RegisterType<ICoinBoxService, CoinBoxService>(new ContainerControlledLifetimeManager());
        }
    }
}
