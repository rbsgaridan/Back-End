using System;
using System.Collections.Generic;

namespace YamangTao.Model.Payroll.Benifits
{
    public class SssPremium
    {
        public int Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool CurrentActive { get; set; }
        public string Description { get; set; }
        public List<SssPremiumBracket> Brackets { get; set; }
    }
}