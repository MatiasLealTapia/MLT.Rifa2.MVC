using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MLT.Rifa2.MVC.Generic;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.Services;
using MLT.Rifa2.MVC.ViewModel;
using System.Reflection;

namespace MLT.Rifa2.MVC.Controllers
{
    public class ReferentController : Controller
    {
        private readonly IReferentService _referentService;
        private readonly IGenericService _genericService;

        public ReferentController(IReferentService referentService, IGenericService genericService)
        {
            _referentService = referentService;
            _genericService = genericService;
        }
        public async Task<ActionResult> List()
        {
            var list = await _referentService.GetList();
            return View(list.ToList());
        }
        public async Task<ActionResult> Add()
        {
            var model = new ReferentViewModel
            {
                OrganizationListItems = GetOrgs().Result,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Add(ReferentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
                model.OrganizationListItems = GetOrgs().Result;
                return View(model);
            }
            if (Tools.CalculateDV(model.ReferentRUT) != model.ReferentDV)
            {
                TempData["mensajeError"] = "El digito verificador es incorrecto.";
                model.OrganizationListItems = GetOrgs().Result;
                return View(model);
            }
            if (await _genericService.GetReferentByRut(model.ReferentRUT) != null)
            {
                TempData["mensajeError"] = "El rut ya esta en uso.";
                model.OrganizationListItems = GetOrgs().Result;
                return View(model);
            }
            if (await _genericService.GetReferentByEmail(model.ReferentEmail) != null)
            {
                TempData["mensajeError"] = "El email ya esta en uso.";
                model.OrganizationListItems = GetOrgs().Result;
                return View(model);
            }
            var ok = await _referentService.Add(model);
            if (ok)
            {
                TempData["mensajeAgregado"] = "El referido ha sido agregada exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            model.OrganizationListItems = GetOrgs().Result;
            return View(model);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var refObj = _referentService.Get(id).Result;
            var model = new ReferentViewModel
            {
                OrganizationListItems = GetOrgs().Result,
                ReferentId = refObj.ReferentId,
                ReferentRUT = refObj.ReferentRUT,
                ReferentDV = refObj.ReferentDV,
                ReferentFirstName = refObj.ReferentFirstName,
                ReferentLastName = refObj.ReferentLastName,
                ReferentCode = refObj.ReferentCode,
                ReferentEmail = refObj.ReferentEmail,
                ReferentPhone = refObj.ReferentPhone,
                ReferentBirthDay = refObj.ReferentBirthDay,
                OrganizationId = refObj.OrganizationId,
                OrganizationName = refObj.OrganizationName,
                CreationDate = refObj.CreationDate,
                IsActive = refObj.IsActive,
                IsDeleted = false,
            };
            return View(model);
        }
        public async Task<ActionResult> EditByRut(int rut)
        {
            var refObj = _referentService.GetByRut(rut).Result;
            return await Edit(refObj.ReferentId);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ReferentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
                return await Edit(model.ReferentId);
            }
            var ok = await _referentService.Edit(model);
            if (ok)
            {
                TempData["mensajeEditado"] = "El referido ha sido editado exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return await Edit(model.ReferentId);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int idObj)
        {
            var ok = await _referentService.Delete(idObj);
            if (ok)
            {
                TempData["mensajeEliminado"] = "El referido ha sido eliminado exitosamente.";
                return RedirectToAction("List");
            }
            TempData["mensajeError"] = "Ha ocurrido un error inesperado.";
            return await Edit(idObj);
        }
        private async Task<IEnumerable<SelectListItem>> GetOrgs()
        {
            var orgs = await _genericService.GetOrganizations();
            var orgListItems = orgs.Select(ot => new SelectListItem
            {
                Value = ot.Id.ToString(),
                Text = ot.Detail
            });
            return orgListItems;
        }
    }
}
