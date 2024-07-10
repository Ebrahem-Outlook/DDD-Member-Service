using Application.Core.Abstractions.Messaging;

namespace Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
    string FirstName, 
    string LastName,
    string Email,
    string Password) : ICommand<string>;
