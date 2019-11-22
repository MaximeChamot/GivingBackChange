using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GivingBackChange.Ui.Readers
{
    public class CommandReader
    {
        private readonly IGetChangeService _getChangeService;
        private readonly ICoinBoxService _coinBoxService;

        private const string Exit = "exit";
        private const string Change = "change";
        private const string ShowCoinBox = "show";

        public CommandReader(IGetChangeService getChangeService, ICoinBoxService coinBoxService)
        {
            this._getChangeService = getChangeService;
            this._coinBoxService = coinBoxService;
        }

        public async Task ReadCommands()
        {
            var readStr = string.Empty;

            while (readStr.ToLower() != Exit)
            {
                readStr = Console.ReadLine();

                var splitReadKey = readStr?.Split(' ');

                await this.SelectAction(splitReadKey);
            }
        }

        private async Task SelectAction(IReadOnlyList<string> splittedReadKey)
        {
            if (splittedReadKey == null || splittedReadKey.Count == 0)
            {
                return;
            }

            IList<CoinBo> coinBos;

            switch (splittedReadKey[0])
            {
                case Change:
                    if (!decimal.TryParse(splittedReadKey[1], out var remainingChange))
                    {
                        return;
                    }

                    coinBos = await this._getChangeService.GetChange(remainingChange);
                    this.ShowCoins(coinBos);
                    break;
                case ShowCoinBox:
                    coinBos = (await this._coinBoxService.GetCoins()).OrderByDescending(c => c.Value).ToList();
                    this.ShowCoins(coinBos);
                    break;
                default:
                    Console.WriteLine($"Command \"{splittedReadKey[0]}\" not recognized");
                    break;
            }

            Console.WriteLine($"-----------");
        }

        private void ShowCoins(IList<CoinBo> coinsBo)
        {
            var i = 0;

            while (i < coinsBo.Count)
            {
                Console.WriteLine($"{coinsBo[i].Label} (Quantity : {coinsBo[i].Quantity})");
                i++;
            }
        }
    }
}
