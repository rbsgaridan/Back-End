using System.Buffers;
using System;
using System.Collections.Generic;

namespace YamangTao.Model.Auth
{
    public class UserWithRolesForDisplay
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public List<string> Roles { get; set; }
    }
}