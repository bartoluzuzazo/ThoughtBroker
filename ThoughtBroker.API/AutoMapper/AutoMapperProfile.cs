using AutoMapper;
using ThoughtBroker.API.DTOs.UserDTOs.Create;
using ThoughtBroker.API.DTOs.UserDTOs.Get;
using ThoughtBroker.Application.UserServices.Commands;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.API.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserCreateRequest, AddUserCommand>();
        CreateMap<User, UserGetResponse>();
    }
}