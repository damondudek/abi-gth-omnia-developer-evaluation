// User.API/Services/UserPublisher.cs
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

public class UserPublisher : IUserPublisher
{
    private readonly IBus _bus;

    public UserPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishUserUpdated(UserUpdatedDto dto)
    {
        await _bus.Send(new UserUpdatedEvent(dto.userId, dto.username));
    }
}