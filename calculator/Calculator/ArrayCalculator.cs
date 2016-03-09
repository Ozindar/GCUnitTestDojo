namespace Calculator
{
    using System;
    using System.Linq;

    /// <summary>
    ///     Mahtematical operations on an array of integers
    /// </summary>
    public class ArrayCalculator
    {
        /// <summary>
        ///     Adds the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The sum of all the intergers in the array</returns>
        /// <exception cref="System.ArgumentNullException">When parameter is null.</exception>
        /// <exception cref="System.OverflowException">When the sum is more then <see cref="int.MaxValue" />.</exception>
        public int Add(int[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var returnValue = 0;

            returnValue = input.Aggregate(returnValue, (current, i) => checked(current + i));

            return returnValue;
        }
    }
}
