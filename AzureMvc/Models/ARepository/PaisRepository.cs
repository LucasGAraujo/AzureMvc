using AzureMvc.Data;
using System;

namespace AzureMvc.Models.ARepository
{
    public class PaisRepository : IPaisRepository
    {
            private readonly AzureMvcContext _azureMvcContext;

            public PaisRepository(AzureMvcContext azureMvcContext)
            {
            _azureMvcContext = azureMvcContext;
            }

            public IEnumerable<Pais> AllPaises => _azureMvcContext.Paises;

        }
    }

