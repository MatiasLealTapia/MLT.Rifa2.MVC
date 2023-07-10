using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class LogInUserViewModel
    {
        [Required(ErrorMessage = "Nombre de Usuario requerido.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Contraseña requerida.")]
        public string Password { get; set; }
    }
}
