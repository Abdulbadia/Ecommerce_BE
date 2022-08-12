using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.DTO
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
