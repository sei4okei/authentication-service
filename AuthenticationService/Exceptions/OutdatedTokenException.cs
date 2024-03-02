namespace AuthenticationService.Exceptions
{
    public class OutdatedTokenException : Exception
    {
        public string? Type { get; set; }

        public OutdatedTokenException() { }
        public OutdatedTokenException(string type, string message) : base(message) { Type = type; }
        public OutdatedTokenException(string type, Exception inner) : base(type, inner) { }
    }
}
