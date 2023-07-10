using System.ComponentModel.DataAnnotations;

namespace MLT.Rifa2.MVC.ViewModel
{
    public class OrganizationLogIn
    {
        [Required(ErrorMessage = "Nombre de Usuario requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contraseña requerida.")]
        public string Password { get; set; }
    }
}
