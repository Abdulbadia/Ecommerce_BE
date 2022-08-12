using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.DTO
{
    public class RegistrUserDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

       
    }
}
