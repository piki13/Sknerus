using Microsoft.EntityFrameworkCore;

namespace Skapiec.Entities
{
    [Keyless]
    public class Product
    {
        public string name { get; set; }
        public double value { get; set; }
        public string link { get; set; }
    }
}
