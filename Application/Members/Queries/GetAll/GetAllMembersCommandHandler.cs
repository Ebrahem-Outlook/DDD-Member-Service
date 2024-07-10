using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetAll;

internal sealed class GetAllMembersCommandHandler : IQueryHandler<GetAllMembersCommand, List<MemberDTO>>
{
    private readonly ILogger<GetAllMembersCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public GetAllMembersCommandHandler(ILogger<GetAllMembersCommandHandler> logger, IMemberRepository memberRepository)
    {
        _logger = logger;
        _memberRepository = memberRepository;
    }

    public async Task<List<MemberDTO>> Handle(GetAllMembersCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service Start...");

        List<Member>? members = await _memberRepository.GetAllAsync(cancellationToken);

        if (members is null)
        {
            return new List<MemberDTO>();
        }

        List <MemberDTO> memberDTOs = new List<MemberDTO>(members.Count);

        foreach (Member member in members)
        {
            MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email);

            memberDTOs.Add(memberDTO);
        }
        
        _logger.LogInformation("Service Success...");

        return memberDTOs;
    }
}
