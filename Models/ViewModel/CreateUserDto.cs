using System.ComponentModel.DataAnnotations;

namespace CodeComm.Models.ViewModel
{
    public class CreateUserDto
    {

        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string UserPassword { get; set; }
      
        public string? UserProfilePicUrl { get; set; }

        [Compare("UserPassword")]
        public string ConfirmPassword { get; set; }
    }
}
