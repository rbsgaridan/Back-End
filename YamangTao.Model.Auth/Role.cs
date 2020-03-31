
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace YamangTao.Model.Auth
{
    public class Role : IdentityRole<string>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
