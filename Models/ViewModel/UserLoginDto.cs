using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CodeComm.Models.ViewModel
{
    public class UserLoginDto
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}


