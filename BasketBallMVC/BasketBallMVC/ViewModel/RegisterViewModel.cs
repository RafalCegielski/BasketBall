using System.ComponentModel.DataAnnotations;

namespace BasketBallMVC.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Zły format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła się nie zgadzają")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [MaxLength(15, ErrorMessage = "Nick jest zbyt długi")]
        public string Nick { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}