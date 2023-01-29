using System;

namespace EFxceptions.Models.Exceptions
{
    public class UndefinedObjectException : Exception
    {
        public UndefinedObjectException(string message) : base(message) { }
    }
}