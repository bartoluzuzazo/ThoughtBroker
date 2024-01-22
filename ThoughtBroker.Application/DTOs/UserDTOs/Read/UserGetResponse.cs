namespace ThoughtBroker.Application.DTOs.UserDTOs.Read;

public class UserGetResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}