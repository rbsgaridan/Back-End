using System;

namespace YamangTao.Model.Auth
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Desciption { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
    }
}
