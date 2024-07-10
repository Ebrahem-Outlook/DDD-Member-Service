using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdateMember;

internal sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<UpdateMemberCommandHandler> _logger;

    public UpdateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, ILogger<UpdateMemberCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public async Task<string> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service start...");

        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            _logger.LogError("User with spicified Id already exist.");

            throw new InvalidOperationException();
        }

        member.UpdateName(request.FirstName, request.LastName);

        _memberRepository.Update(member);

        string token = _jwtProvider.GenerateToken(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Service Success...");

        return token;
    }
}
