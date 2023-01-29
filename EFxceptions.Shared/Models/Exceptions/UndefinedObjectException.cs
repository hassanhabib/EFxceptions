using System;

namespace EFxceptions.Shared.Models.Exceptions
{
    public class UndefinedObjectException : Exception
    {
        public UndefinedObjectException(string message) : base(message) { }
    }
}