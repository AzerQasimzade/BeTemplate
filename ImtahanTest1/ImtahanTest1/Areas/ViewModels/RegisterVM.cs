

using System.ComponentModel.DataAnnotations;

namespace ImtahanTest1.Areas.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
