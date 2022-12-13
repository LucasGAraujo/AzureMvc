using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureMvc.Components
{
    public class PaisMenu : ViewComponent
    {
        private readonly IPaisRepository _iPaisRepository;
        public PaisMenu(IPaisRepository iPaisRepository)
        {
            _iPaisRepository = iPaisRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _iPaisRepository.AllPaises.OrderBy(c => c.Nome);
            return View(categories);
        }
    }
}
