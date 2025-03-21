using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.Common
{
    public class UserRules : IUserRules
    {
        private readonly IUserRepository _userRepository;

        public UserRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CheckRulesToCreate(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await CheckIsUserExistsByEmail(command.Email, cancellationToken);
            await CheckIsUserExistsByUsername(command.Email, cancellationToken);
        }

        public async Task CheckRulesToUpdate(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingUser is null)
                throw new InvalidOperationException($"User with id {command.Id} does not exists");

            await CheckIsUserExistsByEmail(command.Email, cancellationToken);
            await CheckIsUserExistsByUsername(command.Email, cancellationToken);
        }

        private async Task CheckIsUserExistsByEmail(string email, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (existingUser is not null)
                throw new InvalidOperationException($"User with email {email} already exists");
        }

        private async Task CheckIsUserExistsByUsername(string username, CancellationToken cancellationToken)
        {
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(username, cancellationToken);
            if (existingUserByUsername is not null)
                throw new InvalidOperationException($"User with username {username} already exists");
        }
    }

    public interface IUserRules
    {
        Task CheckRulesToCreate(CreateUserCommand command, CancellationToken cancellationToken);
        Task CheckRulesToUpdate(UpdateUserCommand command, CancellationToken cancellationToken);
    }
}
