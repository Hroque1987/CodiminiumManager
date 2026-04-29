using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.Enums;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;

namespace Identity.Tests;

public class UserTests
{
    [Fact]
    public void Should_Create_User_Successfully()
    {
        var fullName = FullName.Create("John", "Doe").Value;
        var email = Email.Create("valid.email@gmail.com").Value;

        var userResult = User.Create(fullName, email, "123456");

        Assert.True(userResult.IsSuccess);

        var user = userResult.Value;

        Assert.Equal(fullName.FirstName, user.Name.FirstName);
        Assert.Equal(fullName.LastName, user.Name.LastName);
        Assert.Equal(email.Value, user.Email.Value);
        Assert.Equal("123456", user.Password);
    }

    [Fact]
    public void Should_Throw_When_FullName_Is_Null()
    {
        var email = Email.Create("valid.email@gmail.com").Value;

        var ex = Assert.Throws<ArgumentNullException>(() =>
            User.Create(null!, email, "123456")
        );

        Assert.Equal("name", ex.ParamName);
        Assert.Contains(IdentityErrors.UserErrors.UserFullNameEmpty.Message, ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Email_Is_Null()
    {
        var fullName = FullName.Create("John", "Doe").Value;

        var ex = Assert.Throws<ArgumentNullException>(() =>
            User.Create(fullName, null!, "123456")
        );

        Assert.Equal("email", ex.ParamName);
        Assert.Contains(IdentityErrors.UserErrors.UserEmailEmpty.Message, ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Password_Is_Empty()
    {
        var fullName = FullName.Create("John", "Doe").Value;
        var email = Email.Create("valid.email@gmail.com").Value;

        var ex = Assert.Throws<ArgumentNullException>(() =>
            User.Create(fullName, email, "")
        );

        Assert.Equal("password", ex.ParamName);
        Assert.Contains(IdentityErrors.UserErrors.EmptyPassword.Message, ex.Message);
    }

    [Fact]
    public void Should_Change_Email_Successfully()
    {
        var user = CreateValidUser();
        var newEmail = Email.Create("new.email@gmail.com").Value;

        var result = user.ChangeEmail(newEmail);

        Assert.True(result.IsSuccess);
        Assert.Equal(newEmail.Value, user.Email.Value);
    }

    [Fact]
    public void Should_Fail_When_Changing_Email_On_Inactive_User()
    {
        var user = CreateValidUser();
        user.Inactivate();

        var newEmail = Email.Create("new.email@gmail.com").Value;

        var result = user.ChangeEmail(newEmail);

        Assert.True(result.IsFailure);
        Assert.Contains(IdentityErrors.UserErrors.Inactive, result.Errors);
    }

    [Fact]
    public void Should_Inactivate_User_Successfully()
    {
        var user = CreateValidUser();

        var result = user.Inactivate();

        Assert.True(result.IsSuccess);
        Assert.Equal(UserStatus.Inactive, user.Status);
    }

    [Fact]
    public void Should_Fail_When_Inactivating_Already_Inactive_User()
    {
        var user = CreateValidUser();
        user.Inactivate();

        var result = user.Inactivate();

        Assert.True(result.IsFailure);
        Assert.Contains(IdentityErrors.UserErrors.AlreadyInactive, result.Errors);
    }

    [Fact]
    public void Should_Activate_Inactive_User_Successfully()
    {
        var user = CreateValidUser();
        user.Inactivate();

        var result = user.Activate();

        Assert.True(result.IsSuccess);
        Assert.Equal(UserStatus.Active, user.Status);
    }

    [Fact]
    public void Should_Fail_When_Activating_Already_Active_User()
    {
        var user = CreateValidUser();

        var result = user.Activate();

        Assert.True(result.IsFailure);
        Assert.Contains(IdentityErrors.UserErrors.AlreadyActive, result.Errors);
    }

    // Helpers
    private static User CreateValidUser()
    {
        var fullName = FullName.Create("John", "Doe").Value;
        var email = Email.Create("valid.email@gmail.com").Value;

        return User.Create(fullName, email, "123456").Value;
    }
}
