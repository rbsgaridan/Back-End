using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace YamangTao.Model.Auth
{
    public class User : IdentityUser<string>
    {
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Photo> Photos { get; set; }
        // public ICollection<Like> Likers { get; set; }
        // public ICollection<Like> Likees { get; set; }
        // public ICollection<Message> MessagesSent { get; set; }
        // public ICollection<Message> MessagesReceived { get; set; }
        
    }
}
