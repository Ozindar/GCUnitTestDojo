using System;
using DataParser.Helpers;

namespace DataParser.Interfaces
{
    public class MyDateTime : IDateTime
    {
        public MyDateTime()
        {
            GetDateTime = DateTime.Now;
        }

        #region Implementation of IDateTime
        /// <summary>
        /// Gets the get DateTime object.
        /// </summary>
        /// <value>
        /// The get DateTime.
        /// </value>
        public DateTime GetDateTime { get; private set; }
        #endregion
    }
}