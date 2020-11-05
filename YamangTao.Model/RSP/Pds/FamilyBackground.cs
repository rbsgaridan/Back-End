using System.Collections.Generic;

namespace YamangTao.Model.RSP.Pds
{
    public class FamilyBackground
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }

        public string SpouseSurname { get; set; }
        public string SpouseFirstname { get; set; }
        public string SpouseMiddle { get; set; }
        public string Spousesuffix { get; set; }
        public string SpouseOccupation { get; set; }
        public string SpouseEmployer { get; set; }
        public string SpouseEmployerAddress { get; set; }
        public string SpouseEmployesTelNumber { get; set; }
        public string FatherSurname { get; set; }
        public string FatherFirstname { get; set; }
        public string FatherMiddle { get; set; }
        public string Fathersuffix { get; set; }
        public string MotherMaidenName { get; set; }
        public string MotherSurname { get; set; }
        public string MotherFirstname { get; set; }
        public string MotherMiddle { get; set; }
        public string Mothersuffix { get; set; }
        public IEnumerable<Child> Children { get; set; }
    }
}