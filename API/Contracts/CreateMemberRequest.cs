namespace API.Contracts;

public sealed record CreateMemberRequest(
    string FirstName,
    string LastName, 
    string Email,
    string Password);
