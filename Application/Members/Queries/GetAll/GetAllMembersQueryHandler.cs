using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetAll;

internal sealed class GetAllMembersQueryHandler : IQueryHandler<GetAllMembersQuery, List<MemberDTO>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<GetAllMembersQueryHandler> _logger;

    public GetAllMembersQueryHandler(ILogger<GetAllMembersQueryHandler> logger, IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<List<MemberDTO>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service Start...");

        List<Member>? members = await _memberRepository.GetAllAsync(cancellationToken);

        if (members is null)
        {
            _logger.LogError(@$"There are no member yet.");

            return new List<MemberDTO>();
        }

        List <MemberDTO> memberDTOs = new List<MemberDTO>(members.Count);

        foreach (Member member in members)
        {
            MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

            memberDTOs.Add(memberDTO);
        }
        
        _logger.LogInformation("Service Success...");

        return memberDTOs;
    }
}
