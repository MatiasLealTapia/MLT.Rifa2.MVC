using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class ReferentViewModel
    {
        public IEnumerable<SelectListItem> OrganizationListItems;
        public int ReferentId { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public int ReferentRUT { get; set; }
        public string ReferentDV { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public string ReferentFirstName { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public string ReferentLastName { get; set; }
        public string ReferentCode { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public string ReferentEmail { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public int ReferentPhone { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public DateTime ReferentBirthDay { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
