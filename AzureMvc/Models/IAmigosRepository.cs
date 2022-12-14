using static AzureMvc.Models.IAmigosRepository;

namespace AzureMvc.Models
{
    public interface IAmigosRepository
    {
            IEnumerable<Amigos> GetAllFriends();
    }
}
