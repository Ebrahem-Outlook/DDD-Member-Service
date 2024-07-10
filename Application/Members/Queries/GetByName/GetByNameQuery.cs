using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;

namespace Application.Members.Queries.GetByName;

public sealed record GetByNameQuery(string Name) : IQuery<List<MemberDTO>>;
