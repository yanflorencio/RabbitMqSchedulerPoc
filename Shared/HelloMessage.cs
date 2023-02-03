namespace Shared
{
    public record HelloMessage
    {
        public string EmailAddress { get; init; }
        public string Body { get; init; }
    }
}