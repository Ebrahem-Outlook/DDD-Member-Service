using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;

namespace Application.Members.Queries.GetById;

public sealed record GetByIdQuery(Guid MemberId) : IQuery<MemberDTO>;
