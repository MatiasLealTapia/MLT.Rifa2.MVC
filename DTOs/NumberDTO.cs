using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class NumberDTO
    {
        public int NumberId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int NumberPosition { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int RaffleId { get; set; }
        public bool IsBuyed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
