using AutoMapper;
using ScaleSlayer.Application.Authentication;
using ScaleSlayer.Web.Server.Models.Authentication;

namespace ScaleSlayer.Web.Server.Mappings;

public class AuthenticationMappings : Profile
{
    public AuthenticationMappings()
    {
        CreateMap<Domain.UserAggregate.User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        CreateMap<AuthenticationResponse, AuthResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
}