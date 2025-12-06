using System;

namespace BibliothequeDigitale
{
 
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


