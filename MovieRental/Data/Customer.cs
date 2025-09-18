using System.ComponentModel.DataAnnotations;

namespace MovieRental.Data
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
