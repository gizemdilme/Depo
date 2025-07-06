using System.ComponentModel.DataAnnotations;

namespace Depo.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        public string Address { get; set; }

        public int Capacity { get; set; }
    }
}
