using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetById;

internal sealed record GetByIdQueryHandler(
    IMemberRepository MemberRepository, 
    ILogger<GetByIdQueryHandler> Logger) : IQueryHandler<GetByIdQuery, MemberDTO>
{

    public async Task<MemberDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Service start...");

        Member? member = await MemberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            Logger.LogError($"User is spicified Id {request.MemberId} Does not exist.");

            return new MemberDTO(Guid.Empty, default!, default!, default!, default!);
        }

        MemberDTO memberDTO = new(member.Id, member.FirstName.Value, member.LastName.Value, member.Email.Value, member.Password.Value);

        Logger.LogInformation("Service Success...");

        return memberDTO;
    }
}
