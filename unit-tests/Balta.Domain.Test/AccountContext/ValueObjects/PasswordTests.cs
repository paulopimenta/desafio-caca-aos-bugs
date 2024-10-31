using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.SharedContext.Abstractions;
using FluentAssertions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        // arrange
        string password = null;

        // act
        Password expected = Password.ShouldCreate(password);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        // arrange
        string password = String.Empty;

        // act
        Password expected = Password.ShouldCreate(password);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        // arrange
        string password = " ";

        // act
        Password expected = Password.ShouldCreate(password);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        // arrange
        string password = "m#P52s@";

        // act
        Password expected = Password.ShouldCreate(password);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        // arrange
        string password = "m#P52s@ap$V01010101010101010101010101010101010101";

        // act
        Password expected = Password.ShouldCreate(password);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldHashPassword()
    {
        // arrange
        string password = "m#P52s@ap$V";

        // act
        Password expected = Password.ShouldCreate(password);

        // assert
        expected.Hash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        // arrange
        string password = "m#P52s@ap$V";
        string hash = Password.ShouldCreate(password).Hash;

        // act
        bool expected = Password.ShouldMatch(hash, password);

        // assert
        expected.Should().BeTrue();
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        // arrange

        // act
        string expected = Password.ShouldGenerate();

        // assert
        expected.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        // arrange
        //string password = "m#P52s@ap$V";
        Password password = Password.ShouldCreate("m#P52s@ap$V");

        // act
        string expected = password;

        // assert
        expected.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        // arrange
        Password password = Password.ShouldCreate("m#P52s@ap$V");

        // act
        string expected = password.ToString();

        // assert
        expected.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldMarkPasswordAsExpired()
    {
        // arrange
        Password password = Password.ShouldCreate("m#P52s@ap$V");

        // act
        DateTime? expected = password.ExpiresAtUtc;

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
        // arrange
        Password password = Password.ShouldCreate("m#P52s@ap$V");
        DateTime? date = password.ExpiresAtUtc;

        // act
        bool expected = (date is null) ? true : false;

        // assert
        expected.Should().BeTrue();
    }

    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        // arrange
        Password password = Password.ShouldCreate("m#P52s@ap$V");

        // act
        bool expected = password.MustChange;

        // assert
        expected.Should().BeFalse();
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        // arrange
        Password password = Password.ShouldCreate("m#P52s@ap$V");
        bool isChanged = password.MustChange;

        // act
        bool expected = (!isChanged) ? true : false;

        // assert
        expected.Should().BeTrue();
    }
}