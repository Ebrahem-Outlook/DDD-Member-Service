using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetByName;

public sealed record GetByNameQuery(string Name) : IQuery<List<MemberDTO>>;

internal sealed class GetByNameQueryHandler : IQueryHandler<GetByNameQuery, List<MemberDTO>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<GetByNameQueryHandler> _logger;

    public GetByNameQueryHandler(IMemberRepository memberRepository, ILogger<GetByNameQueryHandler> logger)
    {
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<List<MemberDTO>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service Start...");

        List<Member>? members = await _memberRepository.GetByNameAsync(request.Name, cancellationToken);

        if (members is null)
        {
            _logger.LogError(@$"Member with spacifec Name {request.Name} does not exist.");

            return new List<MemberDTO>();
        }

        List<MemberDTO> memberDTOs = new List<MemberDTO>(members.Count);

        foreach (Member member in members)
        {
            MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

            memberDTOs.Add(memberDTO);
        }

        _logger.LogInformation("Service Success...");

        return memberDTOs;
    }
}
