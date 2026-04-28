using CondominiumManager.Identity.Domain.Errors;
using CondominiumManager.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Tests;

public class FullNameTests
{
    [Fact]
    public void Should_Create_FullName_Successfully()
    {
        var firstName = "John";
        var lastName = "Doe";

        var fullName = FullName.Create(firstName, lastName);

        Assert.NotNull(fullName);
        Assert.Equal($"{firstName} {lastName}", fullName.ToString());
    }

    [Fact]
    public void Should_Fail_When_FirstName_Is_Empty()
    {
        var firstName = "";
        var lastName = "Doe";


        var ex = Assert.Throws<ArgumentNullException>(() =>
            FullName.Create(firstName, lastName)
        );



        Assert.Equal("firstName", ex.ParamName);
        Assert.Contains(FullNameErrors.FirstNameEmpty.Message, ex.Message);
    }

    [Fact]
    public void Should_Fail_When_LastName_Is_Empty()
    {
        var firstName = "Jonh";
        var lastName = "";


        var ex = Assert.Throws<ArgumentNullException>(() =>
            FullName.Create(firstName, lastName)
        );


        Assert.Equal("lastName", ex.ParamName);
        Assert.Contains(FullNameErrors.LastNameEmpty.Message, ex.Message);
    }

    [Fact]
    public void Should_Fail_When_FirstName_Exceeds_Max_Length()
    {
       
        var firstName = new string('A', 101);
        var lastName = "Doe";


        var ex = Assert.Throws<ArgumentException>(() =>
            FullName.Create(firstName, lastName)
        );


        Assert.Equal("firstName", ex.ParamName);
        Assert.Contains(FullNameErrors.FirstNameTooLong.Message, ex.Message);


    }

    [Fact]
    public void Should_Fail_When_LastName_Exceeds_Max_Length()
    {
        var firstName = "Jonh";
        var lastName = new string('A', 101);


        var ex = Assert.Throws<ArgumentException>(() =>
            FullName.Create(firstName, lastName)
        );


        Assert.Equal("lastName", ex.ParamName);
        Assert.Contains(FullNameErrors.LastNameTooLong.Message, ex.Message);
    }
}
