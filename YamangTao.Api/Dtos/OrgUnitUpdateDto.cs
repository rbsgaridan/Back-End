namespace YamangTao.Api.Dtos
{
    public class OrgUnitUpdateDto
    {
        public int Id { get; set; }
         public int OrderNumber { get; set; }
        public string UnitName { get; set; }
        public string Code { get; set; }

        public string UnitType { get; set; }
        public string CurrentHeadId { get; set; }
        public string NameOfHead { get; set; }
        public EmployeeDto CurrentHead { get; set; }
        public string Location { get; set; }
        public int? ParentUnitId { get; set; }
        
       
        
    }
}