using System.Runtime.CompilerServices;
using System;
using Microsoft.AspNetCore.Identity;

namespace YamangTao.Model.Auth
{
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
