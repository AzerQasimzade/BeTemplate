using System.ComponentModel.DataAnnotations.Schema;

namespace ImtahanTest1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
