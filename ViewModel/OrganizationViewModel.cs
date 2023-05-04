using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class OrganizationViewModel
    {
        public IEnumerable<SelectListItem> OrganizationTypeListItems;
        public int OrganizationId { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public int OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
