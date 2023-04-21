using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class OrganizationTypeDTO
    {
        public int OrganizationTypeId { get; set; }
        [Required(ErrorMessage = "El campo es requerido.")]
        public string OrganizationTypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
