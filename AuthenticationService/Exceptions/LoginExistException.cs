namespace AuthenticationService.Exceptions
{
    public class LoginExistException : Exception
    {
        public LoginExistException() { }
        public LoginExistException(string message) : base(message) { }
        public LoginExistException(string message, Exception inner) : base(message, inner) { }
    }
}
