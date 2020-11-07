using System;
using System.Collections.Generic;
using YamangTao.Core.Common;
using YamangTao.Core.Document;

namespace YamangTao.Model.RSP.Pds
{
    public class PersonalDataSheet: IIdentifyableEntity<int>, IOwnedEntity
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public Employee Owner { get; set; }
        public string SpouseSurname { get; set; }
        public string SpouseFirstname { get; set; }
        public string SpouseMiddle { get; set; }
        public string SpouseSuffix { get; set; }
        public string SpouseOccupation { get; set; }
        public string SpouseEmployer { get; set; }
        public string SpouseEmployerAddress { get; set; }
        public string SpouseEmployerTelNumber { get; set; }
        public string FatherSurname { get; set; }
        public string FatherFirstname { get; set; }
        public string FatherMiddle { get; set; }
        public string FatherSuffix { get; set; }
        public string MotherMaidenName { get; set; }
        public string MotherSurname { get; set; }
        public string MotherFirstname { get; set; }
        public string MotherMiddle { get; set; }
        public string MotherSuffix { get; set; }
        public string BirthPlace { get; set; }
        public string CivilStatus { get; set; }
        public string OtherCivilStatus { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodType { get; set; }
        public string GsisNumber { get; set; }
        public string HdmfNumber { get; set; }
        public string PhilHealthNumber { get; set; }
        public string SssNumber { get; set; }
        public string TinNumber { get; set; }
        public string AgencyNumber { get; set; }
        public DateTime? DateAccomplished { get; set; }
        public string Citizenship { get; set; }
        public string DualCitizenType { get; set; }
        public string DualCitizenCountry { get; set; }
        
        public List<Address> Addresses { get; set; }
        public List<Identification> IdCards { get; set; }
        // public List<FamilyBackground> FamilyBackgrounds { get; set; }
        public List<EducationalBackground> EducationalBackgrounds { get; set; }
        public List<Child> Children { get; set; }
        public List<Eligibility> Eligibilities { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<VoluntaryWork> VoluntaryWorks { get; set; }
        public List<TrainingAttended> TrainingsAttended { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Recognition> Recognitions { get; set; }
        public List<Membership> Memberships { get; set; }
        public bool ConsanguinityThird { get; set; }
        public bool ConsanguinityFouth { get; set; }
        public string ConsanguinityFouthDetails { get; set; }
        public bool AdministrativeOffense { get; set; }
        public string AdministrativeOffenseDetails { get; set; }
        public bool CriminalCharge { get; set; }
        public DateTime? CriminalChargeDateFiled { get; set; }
        public string CriminalChargeStatus { get; set; }
        public bool Convicted { get; set; }
        public string ConvictedDetails { get; set; }
        public bool SeparatedFromService { get; set; }
        public string SeparatedFromServiceDetails { get; set; }
        public bool ElectionCandidate { get; set; }
        public string ElectionCandidateDetails { get; set; }
        public bool ResignedForElection { get; set; }
        public string ResignedForElectionDetails { get; set; }
        public bool Immigrant { get; set; }
        public string ImmigrantDetails { get; set; }
        public bool IpMember { get; set; }
        public string IpMemberDetails { get; set; }
        public bool PwdMember { get; set; }
        public string PwdMemberDetails { get; set; }
        public bool SoloParent { get; set; }
        public string SoloParentId { get; set; }
        public List<CharacterReference> References { get; set; }
        public string GovIdType { get; set; }
        public string GovIdNumber { get; set; }
        public string GovIdDatePlaceIssued { get; set; }
        public DateTime? DateSubmitted { get; set; } 
        public bool Verified { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }

        public int EntityId => Id;

        public string OwnerId => EmployeeId;
    }
}