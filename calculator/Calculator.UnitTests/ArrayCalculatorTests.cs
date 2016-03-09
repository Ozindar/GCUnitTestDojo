namespace Calculator.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ArrayCalculatorTests
    {
        public static IEnumerable<object[]> InputData
        {
            get
            {
                return new[]
                {
                    // Input array - Expected value
                    new object[] {new int[] {}, 0},
                    new object[] {new[] {42}, 42},
                    new object[] {new[] {1, 2}, 3},
                    new object[] {new[] {43, 33, 5423, 2}, 5501},
                    new object[] {new[] {43, 23, 121, 3, 5, 643, -43, 4321, 3}, 5119}
                };
            }
        }

        [Fact]
        public void Add_WithNullParameter_ThrowsArgumentNullException()
        {
            // Arrange
            var sut = new ArrayCalculator();

            // Act
            var ex = Record.Exception(() => sut.Add(null));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Add_WithMaxIntPlus1_ThrowsException()
        {
            // Arrange
            var sut = new ArrayCalculator();
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
            var sut = new ArrayCalculator();

            // Act
            var actual = sut.Add(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
