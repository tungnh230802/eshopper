using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class ProductViewModel : EditImageViewModel
    {
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Condition { get; set; }
        [StringLength(50)]

        public string Brand { get; set; }
        [Column(TypeName = "decimal"),Range(1,100000000)]
        public float Price { get; set; }

        [Required, Range(1,9999)]
        public int Available { get; set; }
       
    }
}
