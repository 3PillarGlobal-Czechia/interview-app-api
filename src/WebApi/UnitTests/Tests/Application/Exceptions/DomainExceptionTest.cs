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
    }

    [Fact]
    public void DomainException_CanBeThrown()
    {
        static void ThrowFirstConstructor() => throw new InstantiableDomainException();
        static void ThrowSecondConstructor() => throw new InstantiableDomainException("Test message");
        static void ThrowThirdConstructor() => throw new InstantiableDomainException("Test message", new InstantiableDomainException());

        Assert.ThrowsAny<DomainException>(ThrowFirstConstructor);
        Assert.ThrowsAny<DomainException>(ThrowSecondConstructor);
        Assert.ThrowsAny<DomainException>(ThrowThirdConstructor);
    }
}
