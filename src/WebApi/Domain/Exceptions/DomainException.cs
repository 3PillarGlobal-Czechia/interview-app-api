using System;

namespace Domain.Exceptions;

[Serializable]
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}
