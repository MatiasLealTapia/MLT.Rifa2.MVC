using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class OrganizationDTO
    {
        public int OrganizationId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public string OrganizationName { get; set; }
        public int OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
