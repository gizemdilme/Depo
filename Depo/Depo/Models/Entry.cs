using System;
using System.ComponentModel.DataAnnotations;

namespace Depo.Models
{
    public class Entry
    {
        public int EntryId { get; set; }

        [Required]
        public int FabricId { get; set; }

        public Fabric Fabric { get; set; }

        public DateTime? Date { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
