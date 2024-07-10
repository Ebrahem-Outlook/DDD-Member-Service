using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;

namespace Application.Members.Queries.GetByEmail;

public sealed record GetByEmailQuery(string Email) : IQuery<MemberDTO>;
