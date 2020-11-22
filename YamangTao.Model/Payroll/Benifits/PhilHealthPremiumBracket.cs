namespace YamangTao.Model.Payroll.Benifits
{
    public class PhilHealthPremiumBracket
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float MonthlySalaryFrom { get; set; }
        public float MonthlySalaryTo { get; set; }
        public float SalaryBase { get; set; }
        public float Total { get; set; }
        public float EeS { get; set; }
        public float ErS { get; set; }
    }
}