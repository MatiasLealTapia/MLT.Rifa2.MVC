using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class NumberViewModel
    {
        public int NumberId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public int NumberPosition { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido.")]
        public int RaffleId { get; set; }
        public bool IsBuyed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
