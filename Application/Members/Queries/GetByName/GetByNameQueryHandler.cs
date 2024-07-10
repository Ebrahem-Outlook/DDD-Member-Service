using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetByName;

internal sealed class GetByNameQueryHandler(IMemberRepository memberRepository, ILogger<GetByNameQueryHandler> logger) : IQueryHandler<GetByNameQuery, List<MemberDTO>>
{
    public async Task<List<MemberDTO>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service Start...");

        List<Member>? members = await memberRepository.GetByNameAsync(request.Name, cancellationToken);

        if (members is null)
        {
            logger.LogError(@$"Member with spacifec Name {request.Name} does not exist.");

            return new List<MemberDTO>();
        }

        List<MemberDTO> memberDTOs = new List<MemberDTO>(members.Count);

        foreach (Member member in members)
        {
            MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

            memberDTOs.Add(memberDTO);
        }

        logger.LogInformation("Service Success...");

        return memberDTOs;
    }
}
