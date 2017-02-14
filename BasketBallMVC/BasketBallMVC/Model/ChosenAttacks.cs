using System;
using System.ComponentModel.DataAnnotations;

namespace BasketBallMVC.Model
{
    public class ChosenAttacks
    {
        public Guid ChosenAttacksId { get; set; }
        public Guid Exercise1 { get; set; }
        public Guid Exercise2 { get; set; }
        public Guid Exercise3 { get; set; }

        [Required]
        public virtual Character character { get; set; }
    }
}