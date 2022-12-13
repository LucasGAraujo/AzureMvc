using AzureMvc.Data;
using System;

namespace AzureMvc.Models.ARepository
{
    public class EstadoRepository
    {
        private readonly AzureMvcContext _azureMvcContext;
        private readonly ReadOnlyRepository _readOnlyRepository;

        public EstadoRepository(AzureMvcContext azureMvcContext, ReadOnlyRepository readOnlyRepository)
        {
            _azureMvcContext = azureMvcContext;
            _readOnlyRepository = readOnlyRepository;
        }

        public IEnumerable<Estado> GetAllEstados()
        {
            return _readOnlyRepository.GetAll();
        }

        public IEnumerable<Estado> VerTodosEstadosporPais(string Pais)
        {
            return _readOnlyRepository.VerTodosEstadosporPais(Pais);
        }

        public Estado GetEstadoById(int estadoId)
        {
            return _readOnlyRepository.GetById(estadoId);
        }

        public void Save(Estado estado)
        {
            this._azureMvcContext.Estados.Add(estado);
            this._azureMvcContext.SaveChanges();
        }
    }
}
