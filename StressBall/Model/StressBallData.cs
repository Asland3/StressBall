using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StressBall
{
    [Table("StressBallData")]
    public class StressBallData
    {
        [Key]
        public int Id { get; set; }
        public double Speed { get; set; }
        public DateTime DateTimeNow { get; set; }

    }
}