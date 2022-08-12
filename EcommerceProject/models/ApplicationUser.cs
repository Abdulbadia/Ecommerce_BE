using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Address { get; set; }
        public byte[] Image { get; set; }
    }
}
