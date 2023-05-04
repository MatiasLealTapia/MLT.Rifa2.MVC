using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class ReferentDTO
    {
        public int ReferentId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int ReferentRUT { get; set; }
        public string ReferentDV { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string ReferentFirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string ReferentLastName { get; set; }
        public string ReferentCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string ReferentEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int ReferentPhone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public DateTime ReferentBirthDay { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
