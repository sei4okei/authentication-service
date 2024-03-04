namespace BusinessLogicLayer.Exceptions
{
    public class IncorrectInputException : Exception
    {
        public IncorrectInputException() { }
        public IncorrectInputException(string message) : base(message) { }
        public IncorrectInputException(string message, Exception inner) : base(message, inner) { }
    }
}
