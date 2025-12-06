using System;

namespace BibliothequeDigitale
{
    /// <summary>
    /// Custom exception thrown when a document cannot be found
    /// </summary>
    public class DocumentNonTrouveException : Exception
    {
        public DocumentNonTrouveException() : base("Le document n'a pas été trouvé.")
        {
        }

        public DocumentNonTrouveException(string message) : base(message)
        {
        }

        public DocumentNonTrouveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

