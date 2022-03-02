using FluentAssertions;
using Xunit;

namespace MarsRover.Test
{
    public class PlataeuTests
    {
        [Theory]
        [InlineData("4 5")]
        [InlineData("1 2")]
        [InlineData("2 11")]
        [InlineData("1 1")]
        public void TwoNumbersGeneratePlataueWithCorrectSize(string command)
        {
            // Arrange
            ILandingSurface landingSurface = new Plataeu();

            var commandSplit = command.Split(' ');
            var expectedWidth = int.Parse(commandSplit[0]) + 1;
            var expectedHeight = int.Parse(commandSplit[1]) + 1;

            // Act
            landingSurface.Define(expectedWidth, expectedHeight);

            // Assert
            landingSurface.Size.Width.Should().Be(expectedWidth);
            landingSurface.Size.Height.Should().Be(expectedHeight);
        }
    }
}
