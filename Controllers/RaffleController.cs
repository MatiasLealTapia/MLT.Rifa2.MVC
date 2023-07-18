using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;
using System.Data;
using System.Reflection;
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

        [HttpGet]
        public async Task<IActionResult> Payment(int id)
        {
            var buyed = await _raffleService.NumberBuyed(id);
            var url = Url.Action("CreateRafflePayment", "Payment", id);

            // Redirige a la URL
            return Redirect(url);
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

        [HttpGet]
        public async Task<IActionResult> RaffleList()
        {
            var orgUser = User;
            var list = await _raffleService.GetList();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Raffle(int id)
        {
            var raffle = await _raffleService.GetRaffleById(id);
            return View(raffle);
        }
    }
}
