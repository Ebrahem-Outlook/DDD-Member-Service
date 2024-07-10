using Application.Core.Abstractions.Messaging;

namespace Application.Members.Queries.GetAll;

public sealed record GetAllMembersQuery() : IQuery<List<MemberDTO>>;
