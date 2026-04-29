using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Auth;
using CondominiumManager.Identity.Application.Contracts.Commands;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;
using Sharedkernel.Abstractions;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.UseCases;

internal class LoginUserHandler : IUseCaseHandler<LoginUserCommand, string>
{
    private readonly IPasswordService _passwordService;
    private readonly IUserRepository _userRepository;
    private readonly IdentityService _identityService;  


    public LoginUserHandler(IPasswordService passwordService, IUserRepository userRepository,IdentityService identityService) 
    {
        _passwordService = passwordService;
        _userRepository = userRepository;
        _identityService = identityService;
    }

    public async Task<Result<string>> HandleAsync(LoginUserCommand loginUserCommand, CancellationToken ct)
    {
        var emailResult = Email.Create(loginUserCommand.Email);
        if(!emailResult.IsSuccess)
            return Result<string>.Failure([.. emailResult.Errors]);


        var user = await _userRepository.GetUserByEmail(emailResult.Value);

        if (user is null)
            return Result<string>.Failure(IdentityErrors.LoginErrors.InvalidLogin);
        
  
    
        var isPasswordValid = _passwordService.PasswordVerify(loginUserCommand.Password, user.Password);

        if (!isPasswordValid)
            return Result<string>.Failure(IdentityErrors.LoginErrors.InvalidLogin);
     
        var token = await _identityService.GenerateToken(user);

        return Result<string>.Success(token);

    }


}
