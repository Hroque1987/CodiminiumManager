using Sharedkernel.Abstractions;
using BC =  BCrypt.Net.BCrypt;

namespace InfraStructure.PasswordHasher;

public class PasswordService : IPasswordService
{
    public string PasswordHash(string password)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(password);

        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool PasswordVerify(string password, string passwordhash)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(password);

        return BC.Verify(password, passwordhash);
    }
}
