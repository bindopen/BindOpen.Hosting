using System;

namespace BindOpen.Hosting
{
    /// <summary>
    /// This static class lists the BindOpen host load exceptions.
    /// </summary>
    public class BdoHostLoadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BdoHostLoadException class.
        /// </summary>
        public BdoHostLoadException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoHostLoadException class.
        /// </summary>
        /// <param key="message">The message to consider.</param>
        public BdoHostLoadException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoHostLoadException class.
        /// </summary>
        /// <param key="message">The message to consider.</param>
        /// <param key="innerException">The inner exception to consider.</param>
        public BdoHostLoadException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}