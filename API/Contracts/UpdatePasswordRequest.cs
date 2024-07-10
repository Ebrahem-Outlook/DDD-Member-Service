namespace API.Contracts;

public sealed record UpdatePasswordRequest(
    Guid MemberId,
    string Password);
