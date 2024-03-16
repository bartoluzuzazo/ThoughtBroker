using AutoMapper;
using ThoughtBroker.Application.DTOs.OpinionDTOs.Create;
using ThoughtBroker.Application.DTOs.OpinionDTOs.Read;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Create;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Read;
using ThoughtBroker.Application.DTOs.UserDTOs.Create;
using ThoughtBroker.Application.DTOs.UserDTOs.Login;
using ThoughtBroker.Application.DTOs.UserDTOs.Read;
using ThoughtBroker.Application.DTOs.UserDTOs.Update;
using ThoughtBroker.Application.OpinionServices.Commands;
using ThoughtBroker.Application.ThoughtServices.Commands;
using ThoughtBroker.Application.UserServices.Commands;
using ThoughtBroker.Application.UserServices.Queries;
using ThoughtBroker.Domain.Opinions;
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
        CreateMap<Thought, GetThoughtsResponseThought>();
        CreateMap<UserLoginRequest, UserLoginQuery>();
        CreateMap<OpinionCreateRequest, CreateOpinionCommand>();
        CreateMap<UserPutPasswordRequest, UpdatePasswordCommand>();
        CreateMap<Opinion, OpinionsGetResponseOpinion>();
    }
}