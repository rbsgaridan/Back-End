using System;


namespace YamangTao.Api.Dtos
{
    public class UserForListDto
    {
         public string Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
          
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
       
    }
}
