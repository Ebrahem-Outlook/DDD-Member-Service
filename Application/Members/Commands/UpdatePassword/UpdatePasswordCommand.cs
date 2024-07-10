using Application.Core.Abstractions.Messaging;

namespace Application.Members.Commands.UpdatePassword;

public sealed record UpdatePasswordCommand(
    Guid MemberId, 
    string Password) : ICommand<string>;
