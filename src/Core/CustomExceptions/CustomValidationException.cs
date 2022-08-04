using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace Core.CustomExceptions
{
    public class CustomValidationException : ValidationException
    {
        public List<string> Messages { get; set; }

        public CustomValidationException(List<string> messages, IEnumerable<ValidationFailure> errors) : this(errors)
        {
            Messages = messages;
        }
        public CustomValidationException(string message) : base(message)
        {
        }

        public CustomValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
        {
        }

        public CustomValidationException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        public CustomValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }

        public CustomValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
