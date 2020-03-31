using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace YamangTao.Api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }   

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]  
        public string Password { get; set; }

        public string Gender { get; set; }

        public string KnownAs { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;

        }
    }
}