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


        public IActionResult CreatePersonaje() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(Personaje personaje) {
            await this.service.InsertPersonajeAsync(personaje.Nombre, personaje.Imagen);
            return RedirectToAction("ApiPersonajes");
        }


        public IActionResult UpdatePersonaje() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonaje(Personaje personaje) {
            await this.service.UpdatePersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen);
            return RedirectToAction("ApiPersonajes");
        }


        public async Task<IActionResult> DeletePersonaje(int idPersonaje) {
            await this.service.DeletePersonajeAsync(idPersonaje);
            return RedirectToAction("ApiPersonajes");
        }


        public async Task<IActionResult> Test() {
            ViewData["TEST"] = await this.service.TestApiAsync();
            return View();
        }

    }
}
