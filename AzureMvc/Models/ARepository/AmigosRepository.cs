namespace AzureMvc.Models.ARepository
{
    public class AmigosRepository: IAmigosRepository
    {
        private readonly ReadOnlyRepository _readOnlyRepository;

        public AmigosRepository(ReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public IEnumerable<Amigos> GetAllFriends()
        {
            return _readOnlyRepository.GetAllAmigos();
        }
    }
}
