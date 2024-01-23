using MediatR;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Create;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.ThoughtServices.Commands;

public record PostThoughtCommand : IRequest<ThoughtCreateResponse>
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; } = null!;
    public Guid? ParentId { get; set; }
}

public class PostThoughtCommandHandler : IRequestHandler<PostThoughtCommand, ThoughtCreateResponse>
{
    private readonly IThoughtRepository _thoughtRepository;

    public PostThoughtCommandHandler(IThoughtRepository thoughtRepository, IUserRepository userRepository)
    {
        _thoughtRepository = thoughtRepository;
    }
    
    public async Task<ThoughtCreateResponse> Handle(PostThoughtCommand request, CancellationToken cancellationToken)
    {
        var thought = Thought.Create(request.AuthorId, request.Content, request.ParentId);
        await _thoughtRepository.CreateThoughtAsync(thought);
        var response = new ThoughtCreateResponse()
        {
            Id = thought.Id
        };
        return response;
    }
}