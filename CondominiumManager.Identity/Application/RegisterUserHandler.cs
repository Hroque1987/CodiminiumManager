using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Contracts.Responses;
using CondominiumManager.Identity.Application.Errors;
using CondominiumManager.Identity.Application.Mapping;
using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.UserEndPoints;
using Sharedkernel.Abstractions;
using Sharedkernel.DomainEvents;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application;

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

    public async Task<Result<UserResponse>> HandleAsync(UserRequest userRequest, CancellationToken ct)
    {
        var fullname = FullName.Create(userRequest.FirstName, userRequest.LastName);
        var email = Email.Create(userRequest.Email);
        var hashedPassword = _passwordService.PasswordHash(userRequest.Password);
        var user = User.Create(fullname, email, hashedPassword);


        var emailAlreadyExists = await _userRepository.ExistsByEmailAsync(user.Email.Value);

        if (emailAlreadyExists)
            return Result<UserResponse>.Failure(ApplicationErrors.EmailAlreadyExists);

        var savedUser = await _userRepository.RegisterAsync(user);

        await _dispatcher.DispatchAsync(new UserRegisteredEvent(user.Email.Value, user.CreatedAt), ct);

        var userResponse = UserMappings.ToResponse(savedUser);

        return Result<UserResponse>.Success(userResponse);


    }
}

