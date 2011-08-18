namespace YouTrack.For.ReSharper.Infrastructure
{
    #region Using Directives

    using System;
    using System.Runtime.Serialization;

    #endregion

    public class YouTrackException : Exception
    {
        public YouTrackException()
        {
        }

        public YouTrackException(string message) : base(message)
        {
        }

        public YouTrackException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected YouTrackException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}