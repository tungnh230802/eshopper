using ASM_THIEN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class CartItem
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
