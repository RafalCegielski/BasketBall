using System.ComponentModel.DataAnnotations;

namespace BasketBallMVC.ViewModel
{
    public class AdminProfileViewModel
    {
        public string TargetUserName { get; set; }
        [Required]
        [Display(Name = "Złoto")]
        public int Gold { get; set; }
        [Required]
        [Display(Name = "Energia")]
        public int Energy { get; set; }
        [Required]
        [Display(Name = "Zdrowie")]
        public int Health { get; set; }
    }
}