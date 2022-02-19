using System.ComponentModel.DataAnnotations;

namespace ASM_THIEN.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
