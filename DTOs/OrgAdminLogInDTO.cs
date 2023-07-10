using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class OrgAdminLogInDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string OrgAdminEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string OrgAdminPasswordHash { get; set; }
    }
}
