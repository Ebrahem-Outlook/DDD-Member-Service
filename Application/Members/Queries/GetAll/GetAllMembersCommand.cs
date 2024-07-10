using Application.Core.Abstractions.Messaging;

namespace Application.Members.Queries.GetAll;

public sealed record GetAllMembersCommand() : IQuery<List<MemberDTO>>;
