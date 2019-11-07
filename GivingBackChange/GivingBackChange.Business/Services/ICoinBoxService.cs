using System.Collections.Generic;
using System.Threading.Tasks;
using GivingBackChange.Business.BusinessObjects;

namespace GivingBackChange.Business.Services
{
    public interface ICoinBoxService
    {
        /// <summary>
        /// Permet d'obtenir la liste des pièces ainsi que leur quantité dans la caisse du distributeur
        /// de la plus petite valeur jusqu'à la plus grande
        /// </summary>
        /// <returns>La liste des pièces disponibles avec leur quantité</returns>
        Task<IList<CoinBo>> GetCoinsOrderedByAscendingOrderValue();

        /// <summary>
        /// Permet d'obtenir la liste des pièces ainsi que leur quantité dans la caisse du distributeur
        /// de la plus grande valeur jusqu'à la plus petite
        /// </summary>
        /// <returns>La liste des pièces disponibles avec leur quantité</returns>
        Task<IList<CoinBo>> GetCoinsOrderedByDescendingOrderValue();
    }
}
