using System;

namespace DataParser.Interfaces
{
    /// <summary>
    /// Interface so DateTime can be mocked out
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Gets the get DateTime object.
        /// </summary>
        /// <value>
        /// The get DateTime.
        /// </value>
        DateTime GetDateTime { get; }
    }
}