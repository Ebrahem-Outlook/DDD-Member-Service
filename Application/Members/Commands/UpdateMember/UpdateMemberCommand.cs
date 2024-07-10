using Application.Core.Abstractions.Messaging;

namespace Application.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(
    Guid MemberId, 
    string FirstName, 
    string LastName) : ICommand<string>;
