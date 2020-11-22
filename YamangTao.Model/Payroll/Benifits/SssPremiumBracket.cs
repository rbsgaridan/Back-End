namespace YamangTao.Model.Payroll.Benifits
{
    public class SssPremiumBracket
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int SssPremiumId { get; set; }
        public SssPremium ParentSetting { get; set; }
        public float MonthlySalaryFrom { get; set; }
        public float MonthlySalaryTo { get; set; }
        public float MonthlyCredit { get; set; }
        public float ER { get; set; }
        public float EC { get; set; }
        public float ER_Total { get; set; }
        public float EE { get; set; }
        public float TOTAL { get; set; }

    }   
}