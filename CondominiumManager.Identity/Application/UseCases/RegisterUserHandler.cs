using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Contracts.Commands;
using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;
using Sharedkernel.Abstractions;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.UseCases;

internal class RegisterUserHandler : IUseCaseHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
  
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



        return Result<Guid>.Success(user.Id);

    }
}

