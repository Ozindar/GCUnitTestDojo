using Warehouse.Models.WeatherService;
using Xunit;

namespace Warehouse.Models.UnitTests.WeatherService
{
    public class GetValueFromStringWithBracketsTests
    {
        [Fact]
        public void GetValueFromString_SomeValidString_ReturnTheValueBetweenTheBracketsAsDecimal()
        {
            // Arrange
            GetValueFromStringWithBrackets sut = new GetValueFromStringWithBrackets();
            string input = "41 F (5 C)";

            // Act
            decimal result = sut.GetValueFromString(input);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void FUNCTION_GIVEN_SHOULD()
        {
            // Arrange
            
            // Act

            // Assert
        }
    }
}
