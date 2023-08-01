namespace GameCenterAPI.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public string Message { get; set; }
        public List<ValidationExceptionInnerException> Errors { get; set; }
        public ValidationException(string message, List<ValidationExceptionInnerException> errors) : base(message)
        {
            Message = message;
            Errors = errors;
        }
    }

    public class ValidationExceptionInnerException
    {
        public string FailedProperty { get; set; }
        public string Message { get; set; }
    }
}
