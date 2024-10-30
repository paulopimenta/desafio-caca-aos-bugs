using Balta.Domain.SharedContext.Extensions;
using FluentAssertions;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("caca-as-bruxas")]
    [InlineData("halloween")]
    [InlineData("0123456789")]
    public void ShouldGenerateBase64FromString(string text)
    {
        // arrange
        // act
        string expected = StringExtensions.ToBase64(text);

        // assert
        expected.Should().NotBeNullOrEmpty();
    }
}