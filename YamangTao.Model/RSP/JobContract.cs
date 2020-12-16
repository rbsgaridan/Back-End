using System;
using YamangTao.Model.OrgStructure;


namespace YamangTao.Model.RSP
{
    public class JobContract // masis
    {
       public string Id { get; set; } // Code
       public string Status { get; set; } // Regular, Reliever, JO
       public string Code { get; set; }
        //Current employee assigned in this position
        public string CurrentEmployeeId { get; set; }
        public Employee CurrentEmployee { get; set; }
        public string JobPosition { get; set; }
        public DateTime ContractStart { get; set; } // Date Position Started
        public DateTime ContractEnd { get; set; }
        public int OrgUnitId { get; set; }
        public OrgUnit OrgUnit { get; set; }
        public string SalaryType { get; set; } // Daily Monthly Hourly
        public bool WithLawop { get; set; } // for monthly absences
        public float DaysPerYear { get; set; } 
        public float DaysPerMonth { get; set; } 
        public float DaysPerWeek { get; set; } 
        public float HoursPerDay { get; set; } 
        public float RatePerDay { get; set; } 
        public float RatePerHour { get; set; }
        public float AverageMonthlyBasicPay { get; set; }
        // public float EcolaPerMonnt { get; set; }
        public float FiveDaysIncentivePerMonth { get; set; }
        // public float ThirteenthMonthlyPay { get; set; }
        // public float UniformAllowancePerMonth { get; set; } 
        // public float RetirementPayPerMonth { get; set; }
        public float Allowance { get; set; }
        // public string OtherAllowancePeriod { get; set; }
        // public float TotalAmountToGuard { get; set; }

        // Social Benifits
        public bool SSSbasedOnBasicPlusOT { get; set; }
        public float SSSPremiumEE { get; set; } // Monthly Contribution Premium
        public int SssPremiumBracketId { get; set; }
        public float PhilHeatlhPremiumEE { get; set; } // Monthly Contribution 
        public int PhilHealthPremiumBracketId { get; set; }
        public float StateInsuranceFundEC { get; set; } // Monthly Contribution
        public float HDMFpremiumContribution { get; set; } // Monthly Contribution
        public float TotalContributions { get; set; } // Monthly Contribution
        public bool IsActive { get; set; }
        // public bool isActive { 
        //     get 
        //     {
        //         if (ContractStart != null && ContractEnd != null)
        //         {
        //             return DateTime.Compare(DateTime.Now, ContractStart) >= 0 
        //                    && DateTime.Compare(DateTime.Now, ContractEnd) <= 0;
        //         }

        //         if (ContractEnd == null)
        //         {
        //             return true;
        //         }

        //         return false;
                
        //     }  
        // }
    }
}