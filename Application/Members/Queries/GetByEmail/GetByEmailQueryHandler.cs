using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetByEmail;

internal sealed record GetByEmailQueryHandler(
    IMemberRepository MemberRepository,
    ILogger<GetByEmailQueryHandler> Logger) : IQueryHandler<GetByEmailQuery, MemberDTO>
{

    public async Task<MemberDTO> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Service Start...");

        Member? member = await MemberRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (member is null)
        {
            Logger.LogError(@$"Member with spacifec email {request.Email} does not exist.");

            return new MemberDTO(Guid.Empty, default!, default!, default!, default!);
        }

        MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

        Logger.LogInformation("Service Success...");

        return memberDTO;
    }
}
