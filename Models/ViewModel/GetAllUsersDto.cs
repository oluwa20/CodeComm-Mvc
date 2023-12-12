using System;

namespace CodeComm.Models.ViewModel
{
    public class GetAllUsersDto
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string userEmail { get; set; }
        public string userPhone { get; set; }
        public string userProfilePicUrl { get; set; }
        public bool userIsVerified { get; set; }
        public bool userAccountIsDisabled { get; set; }
        public string userCreatedAt { get; set; } // Change the type to string
    }
}
