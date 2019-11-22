using System.Threading.Tasks;

namespace GivingBackChange.Business.Services
{
    public interface IDatabaseService
    {
        Task DeleteAndCreateNewDatabase();

        Task SeedData();
    }
}
