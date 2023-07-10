using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class OrganizationFormDTO
    {
        public int OrganizationFormId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string OrganizationName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string OrganizationEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int OrganizationPhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string OrganizationFormInformation { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public bool IsAcepted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
