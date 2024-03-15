using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Skapiec.Entities
{
    //[Keyless]
    public class Product
    {
        public string Name { get; set; }
        public double Value { get; set; }
        [Key]
        public string Link { get; set; }
        public string ImgUrl {  get; set; } 
        public DateTime CreationTime {  get; set; }
    }
}
