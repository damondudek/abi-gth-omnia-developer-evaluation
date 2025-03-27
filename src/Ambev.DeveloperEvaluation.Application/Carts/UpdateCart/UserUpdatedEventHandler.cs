using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UserUpdatedEventHandler : IHandleMessages<UserUpdatedEvent>
    {
        private readonly ICartRepository _cartRepository;

        public UserUpdatedEventHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(UserUpdatedEvent message)
        {
            var carts = await _cartRepository.GetByUserIdAsync(message.UserId);
            foreach (var cart in carts)
            {
                //cart.UserName = message.User.Username;
                await _cartRepository.UpdateAsync(cart);
            }
        }
    }
}
