using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.DTOs
{
    public class RewardDTO
    {
        public int RewardId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public string RewardImgUrl { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required.")]
        public int RaffleId { get; set; }
        public string RaffleName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
