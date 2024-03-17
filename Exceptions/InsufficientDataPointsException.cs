namespace OutliersIdentifier.Exceptions
{
    public class InsufficientDataPointsException : Exception
    {
        public InsufficientDataPointsException() { }
        public InsufficientDataPointsException(string message) : base(message) { }
        public InsufficientDataPointsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
