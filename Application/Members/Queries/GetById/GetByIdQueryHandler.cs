using Application.Core.Abstractions.Messaging;
using Application.Members.Queries.GetAll;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Queries.GetById;

internal sealed record GetByIdQueryHandler : IQueryHandler<GetByIdQuery, MemberDTO>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<GetByIdQueryHandler> _logger;

    public GetByIdQueryHandler(IMemberRepository memberRepository, ILogger<GetByIdQueryHandler> logger)
    {
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<MemberDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service start...");

        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            _logger.LogError($"User is spicified Id {request.MemberId} Does not exist.");

            return new MemberDTO(Guid.Empty, default!, default!, default!, default!);
        }

        MemberDTO memberDTO = new(member.Id, member.FirstName, member.LastName, member.Email, member.Password);

        _logger.LogInformation("Service Success...");

        return memberDTO;
    }
}
