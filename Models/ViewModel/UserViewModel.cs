using System.ComponentModel.DataAnnotations;
namespace CodeComm.Models.ViewModel
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string? UserProfilePicUrl { get; set; }
        public bool UserIsVerified { get; set; }
        public bool UserAccountIsDisabled { get; set; }
        public DateTime UserCreatedAt { get; set; }
    }
}



