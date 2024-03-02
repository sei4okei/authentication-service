namespace AuthenticationService.Exceptions
{
    public class CreateTokenException : Exception
    {
        public string? Type { get; set; }

        public CreateTokenException() { }
        public CreateTokenException(string type) : base(type) { }
        public CreateTokenException(string type, string message) : base(message) { Type = type; }
        public CreateTokenException(string type, Exception inner) : base(type, inner) { }
    }
}
