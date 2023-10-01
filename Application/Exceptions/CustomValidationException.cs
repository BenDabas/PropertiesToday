namespace Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> ErrorMessages { get; set; }
        public string ErrorMessage { get; set; }
        public CustomValidationException(List<string> errorMessages, string errorMessage)
            :base(errorMessage)
        {
            ErrorMessages = errorMessages;
            ErrorMessage = errorMessage;
        }
    }
}
