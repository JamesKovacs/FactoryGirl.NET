using System;
using System.Runtime.Serialization;

namespace FactoryGirl.NET
{
    public class UnsequenceableTypeException : Exception
    {
        public UnsequenceableTypeException()
        {
        }

        public UnsequenceableTypeException(string message) : base(message) {
        }

        public UnsequenceableTypeException(string message, Exception innerException) : base(message, innerException) {
        }

        protected UnsequenceableTypeException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}