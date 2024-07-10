using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetAll;

internal sealed class GetAllMembersQueryHandler(
    IMemberRepository memberRepository,
    ILogger<GetAllMembersQueryHandler> logger) : IQueryHandler<GetAllMembersQuery, List<MemberDTO>>
{
    public async Task<List<MemberDTO>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service Start...");

        List<Member>? members = await memberRepository.GetAllAsync(cancellationToken);

        if (members is null)
        {
            logger.LogError(@$"There are no member yet.");

            return new List<MemberDTO>();
        }

        List <MemberDTO> memberDTOs = new List<MemberDTO>(members.Count);

        foreach (Member member in members)
        {
            MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

            memberDTOs.Add(memberDTO);
        }
        
        logger.LogInformation("Service Success...");

        return memberDTOs;
    }
}
