namespace BusinessLogicLayer.Exceptions
{
    public class EmptyTokenException : Exception
    {
        public string? Type { get; set; }

        public EmptyTokenException() { }
        public EmptyTokenException(string type, string message) : base(message) { Type = type; }
        public EmptyTokenException(string type, Exception inner) : base(type, inner) { }
    }
}
