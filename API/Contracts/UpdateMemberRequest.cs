namespace API.Contracts;

public sealed record UpdateMemberRequest(
    Guid MemberId,
    string FirstName,
    string LastName);
