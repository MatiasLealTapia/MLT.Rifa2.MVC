using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.Services;
using MLT.Rifa2.MVC.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using MLT.Rifa2.MVC.DTOs;

namespace MLT.Rifa2.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IRaffleService _raffleService;
        private readonly IGenericService _genericService;

        public OrganizationController(IOrganizationService organizationService, IRaffleService raffleService, IGenericService genericService)
        {
            _organizationService = organizationService;
            _raffleService = raffleService;
            _genericService = genericService;
        }
        public async Task<ActionResult> List()
        {
            var list = await _organizationService.GetList();
            return View(list.ToList());
        }
        public async Task<ActionResult> Add()
        {
            var orgTypes = await _genericService.GetOrganizationTypes();
            var orgTypeListItems = orgTypes.Select(ot => new SelectListItem
            {
                Value = ot.Id.ToString(),
                Text = ot.Detail
            });
            var model = new OrganizationViewModel
            {
                OrganizationTypeListItems = orgTypeListItems,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Add(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ok = await _organizationService.Add(model);
            if (ok)
            {
                TempData["mensajeAgregado"] = "La organizacion ha sido agregada exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return View(model);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var org = _organizationService.Get(id).Result;
            var orgTypes = await _genericService.GetOrganizationTypes();
            var orgTypeListItems = orgTypes.Select(ot => new SelectListItem
            {
                Value = ot.Id.ToString(),
                Text = ot.Detail
            });
            var model = new OrganizationViewModel
            {
                OrganizationId = org.OrganizationId,
                OrganizationName = org.OrganizationName,
                OrganizationTypeId = org.OrganizationTypeId,
                OrganizationTypeListItems = orgTypeListItems,
                CreationDate = DateTime.Now,
                IsActive = org.IsActive,
                IsDeleted = org.IsDeleted,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
                return await Edit(model.OrganizationId);
            }
            var ok = await _organizationService.Edit(model);
            if (ok)
            {
                TempData["mensajeEditado"] = "La organizacion ha sido editada exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return await Edit(model.OrganizationId);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int idObj)
        {
            var ok = await _organizationService.Delete(idObj);
            if (ok)
            {
                TempData["mensajeEliminado"] = "La organizacion ha sido eliminada exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Organization")]
        [HttpGet]
        public IActionResult Options()
        {
            return View();
        }

        [Authorize(Roles = "Organization")]
        [HttpGet]
        public async Task<IActionResult> RaffleList()
        {
            var orgUser = User;
            var orgId = int.Parse(orgUser.FindFirstValue("OrganizationId"));
            var list = await _raffleService.GetListByOrganization(orgId);
            return View(list);
        }

        [Authorize(Roles = "Organization")]
        [HttpGet]
        public async Task<IActionResult> Raffle(int raffleId)
        {
            //var raffle = await _raffleService.GetRaffleById(raffleId);
            return View();
        }

        [HttpGet]
        public IActionResult Payment()
        {
            var url = Url.Action("CreatePayment", "Payment");

            // Redirige a la URL
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(OrganizationLogIn model)
        {
            if (ModelState.IsValid)
            {
                // Verificar las credenciales del usuario y obtener los roles
                var orgVM = await VerifyCredentials(model);

                if (orgVM != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                        new Claim(ClaimTypes.Role, "Organization"),
                        new Claim("FirstName", orgVM.OrganizationName),
                        new Claim("OrganizationId", orgVM.OrganizationId.ToString()),
                        new Claim("OrganizationName", orgVM.OrganizationName),
                        new Claim("OrganizationTypeId", orgVM.OrganizationTypeId.ToString()),
                        new Claim("OrganizationTypeName", orgVM.OrganizationTypeName),
                        new Claim("CreationDate", orgVM.CreationDate.ToString("dd/MM/yyyy"))
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // El usuario ha iniciado sesión correctamente
                    return RedirectToAction("Options");
                }

                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos");
            }

            // El modelo no es válido, muestra la vista de inicio de sesión con los errores
            return View(model);
        }
        private async Task<OrganizationViewModel> VerifyCredentials(OrganizationLogIn orgLogin)
        {
            var verifyCred = await _organizationService.Login(orgLogin);
            if (verifyCred != null)
            {
                return verifyCred;
            }
            return null;
        }
    }
}
