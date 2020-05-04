using System;

namespace EFxceptions.Models.Exceptions
{
    public class EFxeptionException : Exception
    {
        public EFxeptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public string UserErrorMessage { get; }
    }

    public class SqlEFxeption : EFxeptionException
    {
        public SqlEFxeption(string message, Exception exception)
            : base(message, exception)
        {

        }        
    }
}