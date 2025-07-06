using System.ComponentModel.DataAnnotations;

namespace Depo.Models
{
    public class Logincs
    {
        [Key]
        public int UserId { get; set; } 

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }

        public bool LoggedStatus { get; set; }
    }
}
