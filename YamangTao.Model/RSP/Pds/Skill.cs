namespace YamangTao.Model.RSP.Pds
{
    public class Skill
    {
        public long Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public PersonalDataSheet Pds { get; set; }
    }
}