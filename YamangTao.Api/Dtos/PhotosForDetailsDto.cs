using System;

namespace YamangTao.Api.Dtos
{
    public class PhotosForDetailsDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Desciption { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public bool IsApproved { get; set; }
        
    }
}
