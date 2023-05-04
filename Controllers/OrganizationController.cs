using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.Services;
using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IGenericService _genericService;

        public OrganizationController(IOrganizationService organizationService, IGenericService genericService)
        {
            _organizationService = organizationService;
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
            if(!ModelState.IsValid)
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
    }
}
