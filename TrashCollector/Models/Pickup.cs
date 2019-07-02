using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Pickup
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Regular Pickup Day")]
        public DayOfWeek? RegularPickupDay { get; set; }
        
        [DisplayName("Pickup Confirmed")]
        public bool PickupConfirmed { get; set; }

        [DisplayName("Extra Pickup Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExtraPickupDay { get; set; }

        [DisplayName("Extra Pickup Confirmed")]
        public bool ExtraPickupConfirmed { get; set; }

        public double Bill { get; set; }

        [DisplayName("Temporary Suspension Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TemporarySuspensionStart { get; set; }

        [DisplayName("Temporary Suspension End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TemporarySuspensionEnd { get; set; }

        



    }
}