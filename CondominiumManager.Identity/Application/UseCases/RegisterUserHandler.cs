using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Contracts.Commands;
using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;
using Sharedkernel.Abstractions;
using Sharedkernel.DomainEvents;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.UseCases;

internal class RegisterUserHandler : IUseCaseHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly IPasswordService _passwordService;

    public RegisterUserHandler(IUserRepository userRepository, IDomainEventDispatcher dispatcher, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _dispatcher = dispatcher;
        _passwordService = passwordService;
    }

    public async Task<Result<Guid>> HandleAsync(RegisterUserCommand command, CancellationToken ct)
    {

        var hashedPassword = _passwordService.PasswordHash(command.Password);

        var emailResult = Email.Create(command.Email);
        var fullNameResult = FullName.Create(command.FirstName, command.LastName);

        if (emailResult.IsFailure || fullNameResult.IsFailure)
            return Result<Guid>.Failure([.. emailResult.Errors, .. fullNameResult.Errors]);
   

        var userResult = User.Create(fullNameResult.Value, emailResult.Value, hashedPassword);

        if (userResult.IsFailure)
            return Result<Guid>.Failure([.. userResult.Errors]);
            
        var emailAlreadyExists = await _userRepository.ExistsByEmailAsync(userResult.Value.Email);

        if (emailAlreadyExists)
            return Result<Guid>.Failure(IdentityErrors.UserErrors.EmailSame);

        
        var user = await _userRepository.RegisterAsync(userResult.Value);

        await _dispatcher.DispatchAsync(new UserRegisteredEvent(user.Email.Value, user.CreatedAt), ct);

        return Result<Guid>.Success(user.Id);

    }
}

