using System;
using System.Collections.Generic;


namespace YamangTao.Dto.Rsp
{
    public class PersonalDataSheetDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public EmployeeDto Owner { get; set; }
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
        
        public IEnumerable<AddressDto> Addresses { get; set; }
        public IEnumerable<IdentificationDto> IdCards { get; set; }
        // public IEnumerable<FamilyBackground> FamilyBackgrounds { get; set; }
        public IEnumerable<EducationalBackgroundDto> EducationalBackgrounds { get; set; }
        public IEnumerable<ChildDto> Children { get; set; }
        public IEnumerable<EligibilityDto> Eligibilities { get; set; }
        public IEnumerable<WorkExperienceDto> WorkExperiences { get; set; }
        public IEnumerable<VoluntaryWorkDto> VoluntaryWorks { get; set; }
        public IEnumerable<TrainingAttendedDto> TrainingsAttended { get; set; }
        public IEnumerable<SkillDto> Skills { get; set; }
        public IEnumerable<RecognitionDto> Recognitions { get; set; }
        public IEnumerable<MembershipDto> Memberships { get; set; }
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
        public IEnumerable<CharacterReferenceDto> References { get; set; }
        public string GovIdType { get; set; }
        public string GovIdNumber { get; set; }
        public string GovIdDatePlaceIssued { get; set; }
        public DateTime? DateSubmitted { get; set; } 
        public bool Verified { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }
        
    }
}