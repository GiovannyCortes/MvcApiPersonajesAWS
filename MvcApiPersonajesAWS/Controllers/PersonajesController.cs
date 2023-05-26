using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesAWS.Models;
using MvcApiPersonajesAWS.Services;

namespace MvcApiPersonajesAWS.Controllers {
    public class PersonajesController : Controller {

        private ServiceApiPersonajes service;

        public PersonajesController(ServiceApiPersonajes service) {
            this.service = service;
        }

        public IActionResult Listado() {
            return View();
        }
        
        public IActionResult Details(int idPersonaje) {
            return View(idPersonaje);
        }

        public async Task<IActionResult> ApiPersonajes() {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Test() {
            ViewData["TEST"] = await this.service.TestApiAsync();
            return View();
        }

    }
}
