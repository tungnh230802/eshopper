using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_THIEN.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "decimal"), Range(1, 100000000)]
        public float Price { get; set; }

        [Required, Range(1, 9999)]
        public int Available { get; set; }
        public string Condition { get; set; }
        public string Brand { get; set; }

        [Required, StringLength(255)]
        public string Image { get; set; }
    }
}
