using System.Runtime.InteropServices;
using System.Linq;
using AutoMapper;
using YamangTao.Model.Auth;
using YamangTao.Api.Dtos;
using YamangTao.Model;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.RSP;
using YamangTao.Api.Dtos.Pms;
using YamangTao.Model.PM;

namespace YamangTao.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
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
            CreateMap<OrgUnitUpdateDto, OrgUnit>();
            
            CreateMap<RatingDto, Rating>().ReverseMap();
            CreateMap<RatingMatrixDto, RatingMatrix>().ReverseMap();
            CreateMap<KpiDto, Kpi>().ReverseMap();
            CreateMap<IpcrDto, Ipcr>().ReverseMap();
            CreateMap<IpcrForCreateDto, Ipcr>();
            CreateMap<Ipcr, IpcrDto>()
                .ForMember(dto => dto.Ratee, opt => opt.MapFrom(ipcr => ipcr.Ratee.FullName))
                .ForMember(dto => dto.Position, opt => opt.MapFrom(ipcr => ipcr.Position.Name))
                .ForMember(dto => dto.Unit, opt => opt.MapFrom(ipcr => ipcr.Unit.UnitName))
                .ForMember(dto => dto.CompiledBy, opt => opt.MapFrom(ipcr => ipcr.CompiledBy.FullName))
                .ForMember(dto => dto.ReviewedBy, opt => opt.MapFrom(ipcr => ipcr.ReviewedBy.FullName))
                .ForMember(dto => dto.ApprovedBy, opt => opt.MapFrom(ipcr => ipcr.ApprovedBy.FullName));
            





            

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
