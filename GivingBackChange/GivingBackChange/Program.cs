using GivingBackChange.Business.Services;
using GivingBackChange.Ui.IoC;
using System;
using System.Threading.Tasks;
using GivingBackChange.Ui.Readers;
using Unity;

namespace GivingBackChange.Ui
{
    internal class Program
    {
        private static void Main()
        {
            InitializeApp();
            RunAppAsync().Wait();
        }

        #region Initialize

        private static async Task InitializeDatabase()
        {
            var databaseService = UnityConfigurationBase.Container.Resolve<IDatabaseService>();

            await databaseService.DeleteAndCreateNewDatabase();
            await databaseService.SeedData();
        }

        private static void InitializeApp()
        {
            UnityConfigurationBase.RegisterDependencies();
            InitializeDatabase().Wait();
        }

        #endregion

        private static async Task RunAppAsync()
        {
            try
            {
                var getChangeService = UnityConfigurationBase.Container.Resolve<IGetChangeService>();
                var coinBoxService = UnityConfigurationBase.Container.Resolve<ICoinBoxService>();
                var commandReader = new CommandReader(getChangeService, coinBoxService);

                await commandReader.ReadCommands();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
