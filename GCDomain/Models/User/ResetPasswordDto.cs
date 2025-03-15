using System.ComponentModel.DataAnnotations;

namespace GCDomain.Models.User
{
    public class ResetPasswordDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
