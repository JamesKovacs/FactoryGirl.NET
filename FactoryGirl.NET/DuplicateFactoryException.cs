using System;
using System.Runtime.Serialization;

namespace FactoryGirl.NET {
    public class DuplicateFactoryException : Exception {
        public DuplicateFactoryException() {
        }

        public DuplicateFactoryException(string message) : base(message) {
        }

        public DuplicateFactoryException(string message, Exception innerException) : base(message, innerException) {
        }

        protected DuplicateFactoryException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}