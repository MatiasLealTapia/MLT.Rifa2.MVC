using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MLT.Rifa2.MVC.ViewModel;
using System.Security.Claims;
using System.Data;
using MLT.Rifa2.MVC.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MLT.Rifa2.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IGenericService _genericService;
        private readonly ILoginService _loginService;

        public LoginController(IGenericService genericService, ILoginService loginService)
        {
            _genericService = genericService;
            _loginService = loginService;
        }

        public async Task<IActionResult> SolicitudInscripOrg()
        {
            var orgTypes = await _genericService.GetOrganizationTypes();
            var orgTypeListItems = orgTypes.Select(ot => new SelectListItem
            {
                Value = ot.Id.ToString(),
                Text = ot.Detail
            });
            var model = new OrganizationFormViewModel
            {
                OrganizationTypeListItems = orgTypeListItems,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SolicitudInscripOrg(OrganizationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ok = await _loginService.PostOrganizationForm(model);
            if (ok)
            {
                TempData["mensajeIngresado"] = "La solicitud ha sido ingresada exitosamente.";
                return RedirectToAction("Index", "Home");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            var orgTypes = await _genericService.GetOrganizationTypes();
            var orgTypeListItems = orgTypes.Select(ot => new SelectListItem
            {
                Value = ot.Id.ToString(),
                Text = ot.Detail
            });
            model.OrganizationTypeListItems = orgTypeListItems;
            return View(model);
        }

        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LogInUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar las credenciales del usuario y obtener los roles
                bool isAuthenticated = VerifyCredentials(model.Username, model.Password, out List<string> roles);

                if (isAuthenticated)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim("FirstName", "Matías")
                    };

                    // Agregar los roles como claims
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // El usuario ha iniciado sesión correctamente
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos");
            }

            // El modelo no es válido, muestra la vista de inicio de sesión con los errores
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginUser", "Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool VerifyCredentials(string username, string password, out List<string> roles)
        {
            if (username == "admin" && password == "admin")
            {
                roles = new List<string> { "Admin" };
                return true;
            }
            else if (username == "matias" && password == "123") {
                roles = new List<string> { "User" };
                return true;
            }

            roles = null;
            return false;
        }
    }
}
