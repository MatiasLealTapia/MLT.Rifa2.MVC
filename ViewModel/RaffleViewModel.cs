using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class RaffleViewModel
    {
        public int RaffleId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public string RaffleName { get; set; }
        public string RaffleDescription { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public int RaffleNumberPrice { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public int RaffleNumbersAmount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public DateTime RaffleBeginDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public DateTime RaffleEndDate { get; set; }
        public IEnumerable<RewardViewModel> Rewards { get; set; }
        public IEnumerable<NumberViewModel> Numbers { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public DateTime RaffleCreationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
