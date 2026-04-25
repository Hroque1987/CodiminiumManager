using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Contracts.Commands;
using CondominiumManager.Identity.Application.Errors;
using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using Sharedkernel.Abstractions;
using Sharedkernel.DomainEvents;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.UseCases;

internal class RegisterUserHandler
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

    public async Task<Result<Guid>> HandleAsync(RegisterUserCommand registerUserCommand, CancellationToken ct)
    {
        var fullname = FullName.Create(registerUserCommand.FirstName, registerUserCommand.LastName);
        var email = Email.Create(registerUserCommand.Email);
        var hashedPassword = _passwordService.PasswordHash(registerUserCommand.Password);
        var user = User.Create(fullname, email, hashedPassword);


        var emailAlreadyExists = await _userRepository.ExistsByEmailAsync(user.Email.Value);

        if (emailAlreadyExists)
            return Result<Guid>.Failure(ApplicationErrors.EmailAlreadyExists);

        var savedUser = await _userRepository.RegisterAsync(user);

        await _dispatcher.DispatchAsync(new UserRegisteredEvent(user.Email.Value, user.CreatedAt), ct);

        return Result<Guid>.Success(savedUser.Id);


    }
}

