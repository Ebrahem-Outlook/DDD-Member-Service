namespace API.Contracts;

public sealed record UpdateEmailRequest(
    Guid MemberId,
    string Email);
