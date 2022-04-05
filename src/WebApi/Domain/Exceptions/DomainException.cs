using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public abstract class DomainException : Exception
{
    protected DomainException() { }

    protected DomainException(string message) : base(message) { }

    protected DomainException(string message, Exception inner) : base(message, inner) { }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
