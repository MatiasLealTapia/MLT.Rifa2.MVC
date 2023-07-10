using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class OrganizationFormViewModel
    {
        public IEnumerable<SelectListItem> OrganizationTypeListItems;

        public int OrganizationFormId { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre de la organización.")]
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "Se requiere el email de la organización.")]
        public string OrganizationEmail { get; set; }
        [Required(ErrorMessage = "Se requiere un numero de contacto de la organización.")]
        public int OrganizationPhoneNumber { get; set; }
        [Required(ErrorMessage = "Debes dar alguna información de referencia de la organización. (Si no, será dificil que se te acepte la solicitud.)")]
        public string OrganizationFormInformation { get; set; }
        [Required(ErrorMessage = "Debes elegir un tipo de organización.")]
        public int OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public bool IsAcepted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
