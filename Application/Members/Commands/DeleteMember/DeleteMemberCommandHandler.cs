using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.DeleteMember;

internal sealed class DeleteMemberCommandHandler : ICommandHandler<DeleteMemberCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    private readonly Logger<DeleteMemberCommandHandler> _logger;

    public DeleteMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, Logger<DeleteMemberCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public async Task<string> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service start...");

        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            _logger.LogError($"User with the specified Id {request.MemberId} does not exist.");

            throw new NullReferenceException();
        }

        _memberRepository.Delete(member);

        string token = _jwtProvider.GenerateToken(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Service start...");

        return token;
    }
}
