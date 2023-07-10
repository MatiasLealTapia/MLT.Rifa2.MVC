using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using System.Data;
using System.Security.Claims;

namespace MLT.Rifa2.MVC.Controllers
{
    public class RaffleController : Controller
    {
        private readonly IRaffleService _raffleService;

        public RaffleController(IRaffleService raffleService)
        {
            _raffleService = raffleService;
        }

        [Authorize(Roles = "Organization")]
        [HttpGet]
        public IActionResult CreateRaffle()
        {
            return View();
        }

        [Authorize(Roles = "Organization")]
        [HttpPost]
        public async Task<IActionResult> CreateRaffle(RaffleViewModel model)
        {
            model.OrganizationName = User.FindFirstValue("FirstName");
            model.OrganizationId = int.Parse(User.FindFirstValue("OrganizationId"));
            model.RaffleCreationDate = DateTime.Now;
            model.IsActive = false;
            model.IsDeleted = false;
            var isOk = await _raffleService.CreateRaffle(model);
            if (isOk)
            {
                TempData["mensajeAgregado"] = "Se creo la rifa correctamente";
                return RedirectToAction("Options","Organization");
            } 
            else
            {
                TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
                return View(model);
            }
        }
    }
}
