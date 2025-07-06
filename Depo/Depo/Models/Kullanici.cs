using System.ComponentModel.DataAnnotations;

namespace Depo.Models
{
    public class Kullanici
    {
        
        public int UserId { get; set; }

     
        public string Mail { get; set; }
        public string Sifre { get; set; }
        public bool LoggedStatus { get; set; }
    }

}