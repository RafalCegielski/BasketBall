using System.ComponentModel.DataAnnotations;

namespace BasketBallMVC.ViewModel
{
    public class LoginViewModel : LayoutViewModel
    {
        [Required(ErrorMessage ="Musisz podać adres email")]
        [EmailAddress(ErrorMessage ="Zły format adresu email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Musisz podać hasło")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}