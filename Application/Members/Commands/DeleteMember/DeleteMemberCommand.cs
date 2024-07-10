using Application.Core.Abstractions.Messaging;

namespace Application.Members.Commands.DeleteMember;

public sealed record DeleteMemberCommand(Guid MemberId) : ICommand<string>;
