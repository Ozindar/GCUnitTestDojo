using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace Calculator.UnitTests
{
    public class ArrayCalculatorTests
    {
        [Fact]
        public void Add_WithNullParameter_ThrowsArgumentNullException()
        {
            // Arrange
            ArrayCalculator sut = new ArrayCalculator();
            
            // Act
            Action act = () => sut.Add(null);

            // Assert
            Assert.Throws<ArgumentNullException>(() => act());
        }

        [Fact]
        public void Add_WithOneValue_ReturnsSameValue()
        {
            // Arrange
            ArrayCalculator sut = new ArrayCalculator();
            int[] input = {42};
            int expected = 42;
            
            // Act
            int actual = sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_WithTwoValue_ReturnsAddedValues()
        {
            // Arrange
            ArrayCalculator sut = new ArrayCalculator();
            int[] input = {42, 8};
            int expected = 50;
            
            // Act
            int actual = sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_WithEmptyArray_Returns0()
        {
            // Arrange
            ArrayCalculator sut = new ArrayCalculator();
            int[] input = {};
            int expected = 0;

            // Act
            int actual = sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_WithMaxIntPlus1_ThrowsException()
        {
            // Arrange
            ArrayCalculator sut = new ArrayCalculator();
            int[] input = {int.MaxValue, 1};

            // Act
            Action act = () => sut.Add(input);

            // Assert
            Assert.Throws<OverflowException>(() => act());
        }

        [Theory]
        [MemberData("InputData")]
        public void Add_SomeValidValues_SumOfValues(int[] input, int expected)
        {
            // Arrange
            ArrayCalculator sut = new ArrayCalculator();

            // Act
            int actual = sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> InputData
        {
            get
            {
                return new[]
                {
                    new object[] {new int[] {1,2} , 3},
                    new object[] {new int[] {43,33,5423,2} , 5501},
                    new object[] {new int[] { 43, 23, 121, 3, 5, 643, 43, 4321, 3}, 5205}
                };
            }
        }
    }
}
