using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetByEmail;

internal sealed record GetByEmailQueryHandler : IQueryHandler<GetByEmailQuery, MemberDTO>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<GetByEmailQueryHandler> _logger;

    public GetByEmailQueryHandler(IMemberRepository memberRepository, ILogger<GetByEmailQueryHandler> logger)
    {
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<MemberDTO> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service Start...");

        Member? member = await _memberRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (member is null)
        {
            _logger.LogError(@$"Member with spacifec email {request.Email} does not exist.");

            return new MemberDTO(Guid.Empty, default!, default!, default!, default!);
        }

        MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

        _logger.LogInformation("Service Success...");

        return memberDTO;
    }
}
