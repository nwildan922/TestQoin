using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task1.Models
{
    public class Test01
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Nama { get; set; }
        public short Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
