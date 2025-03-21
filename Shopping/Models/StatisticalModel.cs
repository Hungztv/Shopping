using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class StatisticalModel
    {
        internal object date;
        internal object revenue;
        internal int orders;

        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public int Revenue { get; set; }
        public int Profit { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
