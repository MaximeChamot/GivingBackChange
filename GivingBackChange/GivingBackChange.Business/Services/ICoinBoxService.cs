using GivingBackChange.Business.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services
{
    public interface ICoinBoxService
    {
        /// <summary>
        /// Permet d'obtenir la liste des pièces ainsi que la quantité disponible
        /// pour chacune des pièces dans la caisse du distributeur
        /// </summary>
        /// <returns>La liste des pièces disponibles avec leur quantité</returns>
        Task<IList<CoinBo>> GetCoins();

        /// <summary>
        /// Permet de mettre à jour un référentiel de pièces
        /// </summary>
        /// <param name="updatedCoins"></param>
        /// <returns></returns>
        Task UpdateCoins(IList<CoinBo> updatedCoins);
    }
}
