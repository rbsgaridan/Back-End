using System;
using System.Collections.Generic;
using YamangTao.Core.Document;

namespace YamangTao.Dto.Rsp
{
    public class PersonalDataSheetDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string CivilStatus { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodType { get; set; }
        public DateTime? DateAccomplished { get; set; }
        public IEnumerable<AddressDto> Addresses { get; set; }
        public IEnumerable<IdentificationDto> IdCards { get; set; }
        // public IEnumerable<FamilyBackground> FamilyBackgrounds { get; set; }
        // public IEnumerable<EducationalBackground> EducationalBackgrounds { get; set; }
        public IEnumerable<EligibilityDto> Eligibilities { get; set; }
        // public IEnumerable<WorkExperience> WorkExperiences { get; set; }
        // public IEnumerable<VoluntaryWork> VoluntaryWorks { get; set; }
        // public IEnumerable<TrainingAttended> TrainingsAttended { get; set; }
        // public IEnumerable<Skill> Skills { get; set; }
        // public IEnumerable<Recognition> Recognitions { get; set; }
        // public IEnumerable<Membership> Memberships { get; set; }
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
        // public IEnumerable<Reference> References { get; set; }
        public string GovIdType { get; set; }
        public string GovIdNumber { get; set; }
        public string GovIdDatePlaceIssued { get; set; }
        public DateTime? DateSubmitted { get; set; } 
        public bool Verified { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }
        public string GetDocumentType()
        {
            return "Personal Data Sheet";
        }
        
    }
}