using System.ComponentModel.DataAnnotations;

namespace BasketBallMVC.ViewModel
{
    public class GameSettingsViewModel : LayoutViewModel
    {
        [Required(ErrorMessage ="To pole jest wymagane, musisz wybrać atak.")]
        public string Exercise1 { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane, musisz wybrać atak.")]
        public string Exercise2 { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane, musisz wybrać atak.")]
        public string Exercise3 { get; set; }

        public bool isThreeOrMoreExercises { get; set; }
    }
}