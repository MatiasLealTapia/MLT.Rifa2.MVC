using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class RewardViewModel
    {
        public int RewardId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public string RewardImgUrl { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public int RaffleId { get; set; }
        public string RaffleName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
