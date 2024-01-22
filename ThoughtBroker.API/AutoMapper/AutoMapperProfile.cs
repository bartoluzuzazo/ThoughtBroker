using AutoMapper;
using ThoughtBroker.API.DTOs.ThoughtDTOs.Create;
using ThoughtBroker.API.DTOs.ThoughtDTOs.Read;
using ThoughtBroker.API.DTOs.UserDTOs.Create;
using ThoughtBroker.API.DTOs.UserDTOs.Login;
using ThoughtBroker.API.DTOs.UserDTOs.Read;
using ThoughtBroker.Application.ThoughtServices.Commands;
using ThoughtBroker.Application.UserServices.Commands;
using ThoughtBroker.Application.UserServices.Queries;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.API.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserCreateRequest, AddUserCommand>();
        CreateMap<User, UserGetResponse>();
        CreateMap<ThoughtCreateRequest, PostThoughtCommand>();
        CreateMap<Thought, GetAllThoughtsResponseThought>();
        CreateMap<UserLoginRequest, UserLoginQuery>();
    }
}