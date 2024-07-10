namespace Application.Members.Queries.GetAll;

public sealed record MemberDTO(
    Guid MemberId, 
    string FirstName,
    string LastName, 
    string Email);
