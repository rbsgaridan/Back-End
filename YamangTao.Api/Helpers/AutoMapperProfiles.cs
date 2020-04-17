using System.Linq;
using AutoMapper;
using YamangTao.Model.Auth;
using YamangTao.Api.Dtos;
using YamangTao.Model;

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
