using System.ComponentModel.DataAnnotations;

namespace ImtahanTest1.Areas.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string UserNameOrEmail { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
