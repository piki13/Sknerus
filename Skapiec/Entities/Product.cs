using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Skapiec.Entities
{
    //[Keyless]
    public class Product
    {
        public string name { get; set; }
        public double value { get; set; }
        [Key]
        public string link { get; set; }
    }
}
