using System.Diagnostics.CodeAnalysis;

namespace Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string message) : base(message)
        {

        }
    }
}
