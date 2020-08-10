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
        public string Id { get; set; }   

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]  
        public string Password { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Mi { get; set; }
        public int CampusId { get; set; }
        public int OrgUnitId { get; set; }
        public string Sex { get; set; }
        public DateTime Birthdate { get; set; }

        public string Street { get; set; }
        public string Purok { get; set; }
        public string Barangay { get; set; }
        public string Municipality { get; set; }
        public string Province { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;

        }
    }
}