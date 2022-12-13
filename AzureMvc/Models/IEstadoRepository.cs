namespace AzureMvc.Models
{
    public interface IEstadoRepository
    {
        IEnumerable<Estado> GetAllEstados();
        Estado GetEstadoById(int estadoId);
        void Save(Estado estado);
        IEnumerable<Estado> VerTodosEstadosporPais(string Pais);
    }
}
