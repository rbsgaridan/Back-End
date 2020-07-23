
using System;

namespace YamangTao.Model.Payroll
{
    public class ClientPosition
    {
        public int Id { get; set; }
        //Current employee assigned in this position
        public string EmployeeId { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string SalaryType { get; set; }
        public bool WithLawop { get; set; }
        public float DaysPerYear { get; set; }
        public float DaysPerMonth { get; set; }
        public float DaysPerWeek { get; set; }
        public float HoursPerDay { get; set; }
        public float RatePerDay { get; set; }
        public float AverageMonthlyBasicPay { get; set; }
        public float EcolaPerMonnt { get; set; }
        public float FiveDaysIncentivePerMonth { get; set; }
        public float ThirteenthMonthlyPay { get; set; }
        public float UniformAllowancePerMonth { get; set; }
        public float RetirementPayPerMonth { get; set; }
        public float OtherAllowance { get; set; }
        public string OtherAllowancePeriod { get; set; }
        public float TotalAmountToGuard { get; set; }

        // Social Benifits
        public bool SSSbasedOnBasicPlusOT { get; set; }
        public float SSSPremiumEE { get; set; }
        public float PhilHeatlhPremiumEE { get; set; }
        public float StateInsuranceFundEC { get; set; }
        public float HDMFpremiumContribution { get; set; }
        public float TotalContributions { get; set; }
        public bool isActive { 
            get 
            {
                if (ContractStart != null)
                {
                    return DateTime.Compare(DateTime.Now, ContractStart) >= 0 
                           && DateTime.Compare(DateTime.Now, ContractEnd) <= 0;
                }
                return false;
                
            }  
        }

    }
}