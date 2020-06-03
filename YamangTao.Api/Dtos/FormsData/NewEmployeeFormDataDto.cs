using System.Collections.Generic;

namespace YamangTao.Api.Dtos.FormsData
{
    public class NewEmployeeFormDataDto
    {
        public List<string> Lastnames { get; set; }
        public List<string> Firstnames { get; set; }
        public List<string> Middles { get; set; }

    }
}