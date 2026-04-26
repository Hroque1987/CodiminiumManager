using CondominiumManager.Identity.Application.Abstractions;
using CondominiumManager.Identity.Application.Contracts.Commands;
using CondominiumManager.Identity.Application.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;
using Sharedkernel.Abstractions;
using Sharedkernel.Results;

namespace CondominiumManager.Identity.Application.UseCases;

internal class LoginUserHandler
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
        var email = Email.Create(loginUserCommand.Email);
        var user = await _userRepository.GetUserByEmail(email);

        if(user is null)
            return Result<string>.Failure(ApplicationErrors.EmailUserNotFound);


        var isPasswordValid = _passwordService.PasswordVerify(loginUserCommand.Password, user.Password);

        if (!isPasswordValid)
            return Result<string>.Failure(ApplicationErrors.InvalidLogin);

        var token = await _identityService.GenerateToken(user);
        return Result<string>.Success(token);



    }
}
