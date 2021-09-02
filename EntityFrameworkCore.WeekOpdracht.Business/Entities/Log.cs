using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.WeekOpdracht.Business.Entities
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string MachineName { get; set; }
        public DateTime Logged { get; set; }

        [MaxLength(50)]
        public string Level { get; set; }
        public string Message { get; set; }

        [MaxLength(250)]
        public string Logger { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }

    }
}
