using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorV2.Models
{
    public class DateAdd
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CurrentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FutureDate { get; set; }
    }
}