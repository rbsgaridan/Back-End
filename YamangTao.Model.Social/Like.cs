using System;
using YamangTao.Model.Auth;

namespace YamangTao.Model.Social
{
    public class Like
    {
        public string LikerId { get; set; }
        public string LikeeId { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }
        
    }
}
