using System;
using System.Threading.Tasks;
using GivingBackChange.Business.IoC;
using GivingBackChange.Business.Services;
using GivingBackChange.Dal.IoC;
using Unity;

namespace GivingBackChange.Ui
{
    class Program
    {
        private static IUnityContainer _container;

        private static IUnityContainer Container => _container = _container ?? new UnityContainer();

        private static void RegisterDependencies()
        {
            UnityConfigurationBusiness.RegisterDependencies(Container);
            UnityConfigurationDal.RegisterDependencies(Container);
        }

        private static void InitializeApp()
        {
            RegisterDependencies();
        }

        static void Main(string[] args)
        {
            InitializeApp();
            RunAppAsync().Wait();
            Console.ReadKey();
        }

        private static async Task RunAppAsync()
        {
            var givingChangeService = Container.Resolve<IGetChangeService>();
            var coins = await givingChangeService.GetChange(6f);
            var i = 0;

            while (i < coins.Count)
            {
                Console.WriteLine($"{coins[i].Label} (Quantity : {coins[i].Quantity})");
                i++;
            }
        }
    }
}
