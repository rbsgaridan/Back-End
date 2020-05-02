using System.Collections.Generic;
using YamangTao.Model.PM;

namespace YamangTao.Model.RSP
{
    public class JobPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SalaryGrade { get; set; }
        public List<Ipcr> Ipcrs { get; set; }
    }
}