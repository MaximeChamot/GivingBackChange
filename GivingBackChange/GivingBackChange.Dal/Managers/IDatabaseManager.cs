using System.Threading.Tasks;

namespace GivingBackChange.Dal.Managers
{
    public interface IDatabaseManager
    {
        Task EnsureCreated();

        Task EnsureDeleted();

        Task Seed();
    }
}
