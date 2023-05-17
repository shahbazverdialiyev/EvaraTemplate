using System.ComponentModel.DataAnnotations;

namespace EvaraTemplate.ViewModels.AccountVM
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
