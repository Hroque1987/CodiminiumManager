using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;

namespace Identity.Tests;

public class EmailTests
{

    [Fact]
    public void Should_Create_Email_Successfully()
    {
        var test = "valid.email@gmail.com";

        var emailResult = Email.Create(test);

        Assert.True(emailResult.IsSuccess);
        Assert.Equal(test.ToLowerInvariant(), emailResult.Value.Value);
    }

    [Fact]
    public void Should_Fail_When_Email_Is_Empty()
    {
        var test = "";

        var emailResult = Email.Create(test);

        Assert.True(emailResult.IsFailure);
        Assert.Contains(IdentityErrors.EmailErrors.Empty, emailResult.Errors);

    }

    [Theory]
    [InlineData("invalid.email")]
    [InlineData("@gmail.com")]
    [InlineData("invalid.email@")]
    public void Should_Fail_When_Email_Has_Invalid_Format(string mail)
    {
        var emailResult = Email.Create(mail);

        Assert.True(emailResult.IsFailure);
        Assert.Contains(IdentityErrors.EmailErrors.InvalidFormat, emailResult.Errors);

    }
}
