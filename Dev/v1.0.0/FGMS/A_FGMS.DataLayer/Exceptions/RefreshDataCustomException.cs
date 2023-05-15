using System.Runtime.Serialization;

namespace A_FGMS.DataLayer.Exceptions
{
    [Serializable]
    public class RefreshDataCustomException : Exception
    {
        public RefreshDataCustomException()
        {
        }

        public RefreshDataCustomException(string? message) : base(message)
        {
        }

        public RefreshDataCustomException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RefreshDataCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}