using System.Data;
using System;

namespace YamangTao.Model
{
    public class Employee
    {
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string MI { get; set; }
        public string Suffix { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string Telephone { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool InActive { get; set; }
        public DateTime? DateHired { get; set; }
        public bool Resigned { get; set; }
        public DateTime? DateResigned { get; set; }
        public bool Terminated { get; set; }
        public DateTime? DateTerminated { get; set; }
        public bool Retired { get; set; }
        public DateTime? DateRetired { get; set; }
        public string FullName 
        { 
            get {
                return Lastname + ", " + Firstname + " " + MI;
            }
        }

        public string GetAge() 
        { 
                return DateTime.Now.Subtract(BirthDate ?? DateTime.Now).ToString();
        }

       
        
    }
}
