using API.APIModels;
using API.DTOs;
using AutoMapper;
using Domain.DomainModels;
using Infrastructure.DatabaseModels;

namespace API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDomainModel>();
        CreateMap<UserDomainModel, User>();

        CreateMap<UserDomainModel, UserApiModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<UserDomainModel, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<UserForCreationDto, UserCreationDomainModel>();

        CreateMap<UserCreationDomainModel, User>();
        CreateMap<UserDomainModel, UserDetailsApiModel>();

        CreateMap<LoginDto, LoginDomainModel>();
        CreateMap<AuthDomainModel, LoginResponseDto>();

        CreateMap<Workout, WorkoutDomainModel>();
        CreateMap<WorkoutDomainModel, Workout>();

        CreateMap<WorkoutForCreationDto, WorkoutCreationDomainModel>();
        CreateMap<WorkoutCreationDomainModel, Workout>();

        CreateMap<WorkoutDomainModel, WorkoutApiModel>();

        CreateMap<WeeklySummaryDomainModel, WeeklySummaryApiModel>();
    }
}