using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class UserUpdatedEvent
    {
        public Guid UserId { get; }
        public string Username { get; }

        public UserUpdatedEvent(Guid userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
