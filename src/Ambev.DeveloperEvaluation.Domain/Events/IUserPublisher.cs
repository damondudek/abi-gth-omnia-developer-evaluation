namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IUserPublisher
    {
        Task PublishUserUpdated(UserUpdatedDto dto);
    }

    public record UserUpdatedDto(Guid userId, string username);
}
