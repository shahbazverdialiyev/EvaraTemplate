using System.ComponentModel.DataAnnotations;

namespace EvaraTemplate.ViewModels.AccountVM
{
    public class RegisterVM
    {
        [Required,MaxLength(16),DataType(DataType.Text)]
        public string Username { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,DataType(DataType.Password),MaxLength(16)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
