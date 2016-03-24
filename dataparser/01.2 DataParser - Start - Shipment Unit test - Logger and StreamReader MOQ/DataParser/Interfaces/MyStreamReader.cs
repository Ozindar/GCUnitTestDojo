using System;
using System.IO;

namespace DataParser.Interfaces
{
    public class MyStreamReader : IStreamReader, IDisposable
    {
        private StreamReader _streamReader;

        public MyStreamReader(string path)
        {
            _streamReader = new StreamReader(path);
        }

        public string ReadLine()
        {
            return _streamReader.ReadLine();
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _streamReader.Dispose();
        }

        #endregion
    }
}