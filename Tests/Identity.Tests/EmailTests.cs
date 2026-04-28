using CondominiumManager.Identity.Domain.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;

namespace Identity.Tests;

public class EmailTests
{

    [Fact]
    public void Should_Create_Email_Successfully()
    {
        var test = "valid.email@gmail.com";

        var email = Email.Create(test);

        Assert.NotNull(email);
        Assert.Equal(test.ToLowerInvariant(), email.Value);
    }

    [Fact]
    public void Should_Fail_When_Email_Is_Empty()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
           Email.Create("")
        );

        Assert.Equal("email", ex.ParamName);
        Assert.Contains(EmailErrors.Empty.Message, ex.Message);

    }

    [Theory]
    [InlineData("invalid.email")]
    [InlineData("@gmail.com")]
    [InlineData("invalid.email@")]
    public void Should_Fail_When_Email_Has_Invalid_Format(string mail)
    {


        var ex = Assert.Throws<ArgumentException>(() =>
           Email.Create(mail)
        );

        Assert.Equal("email", ex.ParamName);
        Assert.Contains(EmailErrors.InvalidFormat.Message, ex.Message);

    }
}
