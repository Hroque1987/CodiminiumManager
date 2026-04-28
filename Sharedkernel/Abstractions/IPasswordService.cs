namespace Sharedkernel.Abstractions;

public interface IPasswordService
{
    string PasswordHash(string password);

    bool PasswordVerify(string password, string passwordhash);

}
