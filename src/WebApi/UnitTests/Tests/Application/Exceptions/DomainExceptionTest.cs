using Domain.Exceptions;
using System;
using System.Runtime.Serialization;
using Xunit;

namespace UnitTests.Tests.Application.Exceptions;    

public class DomainExceptionTest
{
    private sealed class InstantiableDomainException : DomainException
    {
        public InstantiableDomainException() { }

        public InstantiableDomainException(string message) : base(message) { }

        public InstantiableDomainException(string message, Exception inner) : base(message, inner) { }

        public InstantiableDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Fact]
    public void DomainException_CanBeThrown()
    {
        static void Throw() => throw new InstantiableDomainException("Test message");

        Assert.ThrowsAny<DomainException>(Throw);
    }
}
