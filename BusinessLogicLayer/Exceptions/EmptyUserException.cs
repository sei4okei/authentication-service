namespace BusinessLogicLayer.Exceptions
{
    public class EmptyUserException : Exception
    {
        public EmptyUserException() { }
        public EmptyUserException(string message) : base(message) { }
        public EmptyUserException(string message, Exception inner) : base(message, inner) { }
    }
}
