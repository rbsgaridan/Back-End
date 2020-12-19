using System.Runtime.InteropServices;
using System.Linq;
using AutoMapper;
using YamangTao.Model.Auth;
using YamangTao.Dto;
using YamangTao.Model;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.RSP;
using YamangTao.Dto.Pms;
using YamangTao.Model.PM;
using YamangTao.Model.RSP.Pds;
using YamangTao.Dto.Rsp;
using YamangTao.Model.LND;
using YamangTao.Dto.LND;
using YamangTao.Model.PM.Template;
using YamangTao.Dto.Pms.Template;

namespace YamangTao.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.CurrentCampus, opt => 
                    opt.MapFrom(src => src.CurrentCampus.Campus))
                .ForMember(dest => dest.CurrentUnit, opt => 
                    opt.MapFrom(src => src.CurrentUnit.UnitName));
                
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.CurrentCampus, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUnit, opt => opt.Ignore());
                
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });
            // .ForMember(dest => dest.Age, opt => {
            //     opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            // });
            
            CreateMap<User, UserForDetailsDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });

            CreateMap<BranchCampus, BranchDto>().ReverseMap();
            CreateMap<JobPosition, JobPositionDto>().ReverseMap();
            CreateMap<OrgUnit, OrgUnitDto>()
                .ForMember(dest => dest.ParentUnitId, 
                opt => opt.MapFrom(source => source.ParentUnit.Id));
            
            CreateMap<OrgUnit, OrgUnitListDto>().ReverseMap();
            CreateMap<OrgUnit, OrgUnitDto>().ReverseMap();
            CreateMap<OrgUnitUpdateDto, OrgUnit>();
            

            //IPCR Templates
            CreateMap<IpcrTemplateFullDto, IpcrTemplate>();
                
            CreateMap<IpcrTemplate, IpcrTemplateDto>().ReverseMap();
            // CreateMap<KpiTemplate, KpiTemplateDto>().ReverseMap();
            CreateMap<KpiTemplate, KpiTemplateDto>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path.ToString()))
                .ForMember(dest => dest.KpiType, opt => opt.MapFrom(src => src.KpiType.Description));
                
            CreateMap<KpiTemplateDto, KpiTemplate>()
                .ForMember(dest => dest.Path, opt => opt.Ignore())
                .ForMember(dest => dest.KpiType, opt => opt.Ignore());
                
            CreateMap<RatingMatrixTemplate, RatingMatrixTemplateDto>();
            CreateMap<RatingMatrixTemplateDto, RatingMatrixTemplate>()
                .ForMember(dest => dest.Kpi, opt => opt.Ignore());

            CreateMap<RatingTemplate, RatingTemplateDto>();
            CreateMap<RatingTemplateDto, RatingTemplate>()
                .ForMember(dest => dest.Matrix, opt => opt.Ignore());
            CreateMap<KpiType, KpiTypeDto>().ReverseMap();

            
            CreateMap<RatingDto, Rating>().ReverseMap();
            CreateMap<RatingMatrixDto, RatingMatrix>().ReverseMap();
            CreateMap<KpiDto, Kpi>()
                .ForMember(m => m.KpiType, opt =>  opt.Ignore());
                
            CreateMap<Kpi, KpiDto>()
                .ForMember(m => m.KpiType, opt =>  opt.MapFrom(src => src.KpiType.Description));

            CreateMap<IpcrDto, Ipcr>()
                .ForMember(m => m.Ratee, opt => opt.Ignore())
                .ForMember(m => m.Unit, opt => opt.Ignore())
                .ForMember(m => m.Position, opt => opt.Ignore());
                
            
            CreateMap<Ipcr, IpcrDto>()
                .ForMember(m => m.Ratee, opt => opt.MapFrom(src => src.Ratee.FullName))
                .ForMember(m => m.Position, opt => opt.MapFrom(src => src.Position.Name))
                .ForMember(m => m.Unit, opt => opt.MapFrom(src => src.Unit.UnitName));
                
            
            
            // CreateMap<IpcrForCreateDto, Ipcr>();
            // CreateMap<Ipcr, IpcrDto>()
            //     .ForMember(dto => dto.Ratee, opt => opt.MapFrom(ipcr => ipcr.Ratee.FullName))
            //     .ForMember(dto => dto.Position, opt => opt.MapFrom(ipcr => ipcr.Position.Name))
            //     .ForMember(dto => dto.Unit, opt => opt.MapFrom(ipcr => ipcr.Unit.UnitName))
            //     .ForMember(dto => dto.CompiledBy, opt => opt.MapFrom(ipcr => ipcr.CompiledBy.FullName))
            //     .ForMember(dto => dto.ReviewedBy, opt => opt.MapFrom(ipcr => ipcr.ReviewedBy.FullName))
            //     .ForMember(dto => dto.ApprovedBy, opt => opt.MapFrom(ipcr => ipcr.ApprovedBy.FullName))
            //     .ReverseMap(); 
            // CreateMap<Ipcr, IpcrForListDto>()
            //     .ForMember(dto => dto.Ratee, opt => opt.MapFrom(ipcr => ipcr.Ratee.FullName))
            //     .ForMember(dto => dto.Position, opt => opt.MapFrom(ipcr => ipcr.Position.Name))
            //     .ForMember(dto => dto.Unit, opt => opt.MapFrom(ipcr => ipcr.Unit.UnitName))
            //     .ForMember(dto => dto.CompiledBy, opt => opt.MapFrom(ipcr => ipcr.CompiledBy.FullName))
            //     .ForMember(dto => dto.ReviewedBy, opt => opt.MapFrom(ipcr => ipcr.ReviewedBy.FullName))
            //     .ForMember(dto => dto.ApprovedBy, opt => opt.MapFrom(ipcr => ipcr.ApprovedBy.FullName));

            // PDS
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CharacterReference, CharacterReferenceDto>().ReverseMap();
            CreateMap<Child, ChildDto>().ReverseMap();
            CreateMap<EducationalBackground, EducationalBackgroundDto>().ReverseMap();
            CreateMap<Eligibility, EligibilityDto>().ReverseMap();
            CreateMap<Identification, IdentificationDto>().ReverseMap();
            CreateMap<Membership, MembershipDto>().ReverseMap();
            CreateMap<Recognition, RecognitionDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<TrainingAttended, TrainingAttendedDto>().ReverseMap();
            CreateMap<VoluntaryWork, VoluntaryWorkDto>().ReverseMap();
            CreateMap<WorkExperience, WorkExperienceDto>().ReverseMap();
            CreateMap<PersonalDataSheet, PersonalDataSheetDto>().ReverseMap();
            
            //LND
            CreateMap<Activity, ActivityDto>().ForMember(src => src.ActivityType, opt => opt.MapFrom(act => act.ActivityType.Description));
            CreateMap<ActivityDto, Activity>().ForMember(src => src.ActivityType, opt => opt.Ignore());
            CreateMap<ActivityType, ActivityTypeDto>().ReverseMap();
            
            CreateMap<Certificate, CertificateDto>().ForMember(src => src.CertificateType, opt => opt.MapFrom(act => act.CertificateType.Name));
            CreateMap<CertificateDto, Certificate>().ForMember(src => src.CertificateType, opt => opt.Ignore());
            CreateMap<CertificateType, CertificateTypeDto>().ReverseMap();
            CreateMap<TrainingEffectivenessAssessment, TrainingEffectivenessAssessmentDto>().ReverseMap();
            

            // .ForMember(dest => dest.Age, opt => {
            //     opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            // });
            CreateMap<Photo, PhotosForDetailsDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            // CreateMap<UserForRegisterDto, User>()
            //      .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Username));
            
            // CreateMap<EmployeeToReturnDto, Employee>().ReverseMap();
            // CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            // CreateMap<MessageForCreationDto, Message>().ReverseMap();
            // CreateMap<Message, MessageToReturnDto>()
            //     .ForMember(m => m.SenderPhotoUrl, opt => opt
            //         .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
            //     .ForMember(m => m.RecipientPhotoUrl, opt => opt
            //         .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
            // CreateMap<SupplierDto, Supplier>().ReverseMap();
            // CreateMap<SupplierForReportDto, Supplier>().ReverseMap();
            // CreateMap<Truck, TruckToReturnDto>().ReverseMap();
            // CreateMap<Delivery, DeliveryDto>().ReverseMap();
            // CreateMap<Delivery, DeliveryToCreateDto>().ReverseMap();
            // CreateMap<ProjectForReportDto, Project>().ReverseMap();
            // CreateMap<ProjectDto, Project>().ReverseMap();

        
        }
    }
}
