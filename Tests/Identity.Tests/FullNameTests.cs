using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;

namespace Identity.Tests;

public class FullNameTests
{
    [Fact]
    public void Should_Create_FullName_Successfully()
    {
        var firstName = "John";
        var lastName = "Doe";

        var fullNameResult = FullName.Create(firstName, lastName);

        Assert.True(fullNameResult.IsSuccess);
        Assert.Equal($"{firstName} {lastName}", fullNameResult.Value.ToString());
    }

    [Fact]
    public void Should_Fail_When_FirstName_Is_Empty()
    {
        var firstName = "";
        var lastName = "Doe";

        var fullNameResult = FullName.Create(firstName, lastName);

        Assert.True(fullNameResult.IsFailure);
        Assert.Contains(IdentityErrors.FullNameErrors.FirstNameEmpty, fullNameResult.Errors);
    }

    [Fact]
    public void Should_Fail_When_LastName_Is_Empty()
    {
        var firstName = "Jonh";
        var lastName = "";

        var fullNameResult = FullName.Create(firstName, lastName);

        Assert.True(fullNameResult.IsFailure);
        Assert.Contains(IdentityErrors.FullNameErrors.LastNameEmpty, fullNameResult.Errors);
    }

    [Fact]
    public void Should_Fail_When_FirstName_Exceeds_Max_Length()
    {
       
        var firstName = new string('A', 101);
        var lastName = "Doe";

        var fullNameResult = FullName.Create(firstName, lastName);

        Assert.True(fullNameResult.IsFailure);
        Assert.Contains(IdentityErrors.FullNameErrors.FirstNameTooLong, fullNameResult.Errors);


    }

    [Fact]
    public void Should_Fail_When_LastName_Exceeds_Max_Length()
    {
        var firstName = "Jonh";
        var lastName = new string('A', 101);

        var fullNameResult = FullName.Create(firstName, lastName);

        Assert.True(fullNameResult.IsFailure);
        Assert.Contains(IdentityErrors.FullNameErrors.LastNameTooLong, fullNameResult.Errors);
    }
}
