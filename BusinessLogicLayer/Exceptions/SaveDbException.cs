namespace BusinessLogicLayer.Exceptions
{
    public class SaveDbException : Exception
    {
        public SaveDbException() { }
        public SaveDbException(string message) : base(message) { }
        public SaveDbException(string message, Exception inner) : base(message, inner) { }
    }
}
