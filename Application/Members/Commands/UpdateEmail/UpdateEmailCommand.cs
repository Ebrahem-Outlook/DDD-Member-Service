using Application.Core.Abstractions.Messaging;

namespace Application.Members.Commands.UpdateEmail;

public sealed record UpdateEmailCommand(
    Guid MemberId, 
    string Email) : ICommand<string>;
