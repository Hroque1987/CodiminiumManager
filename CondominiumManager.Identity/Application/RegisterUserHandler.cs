using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Commands;
using CondominiumManager.Identity.Application.Contracts.Responses;
using CondominiumManager.Identity.Application.Mapping;
using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.UserEndPoints;
using Sharedkernel.DomainEvents;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application;

internal class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> HandleAsync(RegisterUserCommand registerUserCommand)
    {
        var fullname = FullName.Create(registerUserCommand.FirstName, registerUserCommand.LastName);

        var email = Email.Create(registerUserCommand.Email);

        var user = User.Create(fullname.Value, email.Value);

        Result<User> result = await _userRepository.RegisterAsync(user.Value);

        if(result.IsFailure) 
            return Result<UserResponse>.Failure(result.Errors.ToList());

        var userResponse = UserMappings.ToResponse(result.Value);

        return userResponse;

    }
}



//internal sealed class RegisterUserHandler
//{
//    private readonly IUserRepository _userRepository;
//    private readonly IDomainEventDispatcher _dispatcher;

//    public RegisterUserHandler(
//        IUserRepository userRepository,
//        IDomainEventDispatcher dispatcher)
//    {
//        _userRepository = userRepository;
//        _dispatcher = dispatcher;
//    }

//    public async Task<Result<UserResponse>> HandleAsync(UserRequest request, CancellationToken ct)
//    {
//        // 1. Domain creation
//        var fullName = FullName.Create(request.FirstName, request.LastName);
//        var email = Email.Create(request.Email);

//        var userResult = User.Create(fullName.Value, email.Value);
//        if (userResult.IsFailure)
//            return Result<UserResponse>.Failure(userResult.Errors);

//        var user = userResult.Value;

//        // 2. Persist
//        var saveResult = await _userRepository.AddAsync(user, ct);

//        if (saveResult.IsFailure)
//            return Result<UserResponse>.Failure(saveResult.Errors);

//        // 3. Domain event (after successful persistence)
//        await _dispatcher.DispatchAsync(
//            new UserRegisteredEvent(user.Id, user.Email.Value),
//            ct);

//        // 4. Response mapping
//        return UserMappings.ToResponse(user);
//    }
//}