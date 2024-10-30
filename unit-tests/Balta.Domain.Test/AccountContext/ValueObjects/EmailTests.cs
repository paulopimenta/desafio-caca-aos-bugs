using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.SharedContext.Abstractions;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Mail;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("TESTE@BALTA.IO")]
    [InlineData("teste@balta.io")]
    public void ShouldLowerCaseEmail(string email)
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().NotBeNull();
        expected.Address.Should().NotBeUpperCased();
    }

    [Theory]
    [InlineData(" teste@balta.io")]
    [InlineData("teste@balta.io ")]
    public void ShouldTrimEmail(string email)
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().NotBeNull();
        expected.Address.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        string? email = null;

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        string? email = String.Empty;

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().NotBeNull();
        expected.Address.Should().BeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("tes=te@balta.io")]
    [InlineData("teste @balta.io")]
    public void ShouldFailIfEmailIsInvalid(string email)
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().BeNull();
    }

    [Fact]
    public void ShouldPassIfEmailIsValid()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        string? email = "teste@balta.io";

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().NotBeNull();
        expected.Address.Should().NotBeNullOrWhiteSpace();
        expected.Hash.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ShouldHashEmailAddress()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        string? email = "teste@balta.io";

        // act
        Email expected = Email.ShouldCreate(email, dateTimeProvider.Object);

        // assert
        expected.Should().NotBeNull();
        expected.Hash.Should().NotBeEmpty();
        expected.Hash.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        Email email = Email.ShouldCreate("teste@balta.io", dateTimeProvider.Object);

        // act
        string expected = email;

        // assert
        expected.Should().NotBeNull();
        expected.Should().Be("teste@balta.io");
        expected.Should().BeEquivalentTo("teste@balta.io");
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        Email email = Email.ShouldCreate("teste@balta.io", dateTimeProvider.Object);

        // act
        string expected = email.ToString();

        // assert
        expected.Should().NotBeNull();
        expected.Should().Be("teste@balta.io");
        expected.Should().BeEquivalentTo("teste@balta.io");
    }

    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        // arrange
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
        Email email = Email.ShouldCreate("teste@balta.io", dateTimeProvider.Object);

        // act
        string expected = email.ToString();

        // assert
        expected.Should().NotBeNull();
        expected.Should().Be("teste@balta.io");
        expected.Should().BeEquivalentTo("teste@balta.io");
    }
}