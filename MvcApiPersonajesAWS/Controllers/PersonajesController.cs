using Microsoft.AspNetCore.Mvc;

namespace MvcApiPersonajesAWS.Controllers {
    public class PersonajesController : Controller {

        public IActionResult Listado() {
            return View();
        }
        
        public IActionResult Details(int idPersonaje) {
            return View(idPersonaje);
        }

    }
}
