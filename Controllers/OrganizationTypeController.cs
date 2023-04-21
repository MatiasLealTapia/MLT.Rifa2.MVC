using Microsoft.AspNetCore.Mvc;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.ViewModel;

namespace MLT.Rifa2.MVC.Controllers
{
    public class OrganizationTypeController : Controller
    {
        private readonly IOrganizationTypeService _organizationTypeService;
        public OrganizationTypeController(IOrganizationTypeService organizationTypeService)
        {
            _organizationTypeService = organizationTypeService;
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(OrganizationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ok = await _organizationTypeService.Add(model);
            if (ok)
            {
                TempData["mensajeAgregado"] = "El tipo de organizacion ha sido agregado exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return View(model);
        }
        public async Task<ActionResult> List()
        {
            var orgTypeList = await _organizationTypeService.GetList();
            return View(orgTypeList.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var sexoAEditar = await _organizationTypeService.Get(id);
            return View(sexoAEditar);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(OrganizationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ok = await _organizationTypeService.Edit(model);
            if (ok)
            {
                TempData["mensajeEditado"] = "El tipo de organizacion ha sido editado exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int idObj)
        {
            var ok = await _organizationTypeService.Delete(idObj);
            if (ok)
            {
                TempData["mensajeEliminado"] = "El tipo de organizacion ha sido eliminado exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return RedirectToAction("List");
        }
    }
}
