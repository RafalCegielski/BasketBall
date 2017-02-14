using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketBallMVC.Model
{
    public class TraningRoom
    {
        [Key, ForeignKey("Character")]
        public Guid TraningRoomID { get; set; }

        public virtual List<TraningRoomByDevice> TraningRoomByDevices { get; set; }
        public virtual List<TraningRoomByExercise> TraningRoomByExercises { get; set; }
        public virtual Character Character { get; set; }
    }
}
