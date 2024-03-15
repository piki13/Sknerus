using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Skapiec.Entities
{
    //[Keyless]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string Link { get; set; }
        public string ImgUrl {  get; set; } 
        public DateTime CreationTime {  get; set; }
    }
}
