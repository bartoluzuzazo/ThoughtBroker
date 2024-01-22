﻿using MediatR;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Create;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.ThoughtServices.Commands;

public record PostThoughtCommand : IRequest<ThoughtCreateResponse>
{
    public string Content { get; set; } = null!;
    public Guid? ParentId { get; set; }
}

public class PostThoughtCommandHandler : IRequestHandler<PostThoughtCommand, ThoughtCreateResponse>
{
    private readonly IThoughtRepository _thoughtRepository;
    private readonly IUserRepository _userRepository;

    public PostThoughtCommandHandler(IThoughtRepository thoughtRepository, IUserRepository userRepository)
    {
        _thoughtRepository = thoughtRepository;
        _userRepository = userRepository;
    }
    
    public async Task<ThoughtCreateResponse> Handle(PostThoughtCommand request, CancellationToken cancellationToken)
    {
        //TODO: REMOVE THIS
        var users = await _userRepository.GetAllUsersAsync();
        var author = users.First();
        
        var thought = Thought.Create(author.Id, request.Content, request.ParentId);

        await _thoughtRepository.CreateThoughtAsync(thought);

        var response = new ThoughtCreateResponse()
        {
            Id = thought.Id
        };
        return response;
    }
}