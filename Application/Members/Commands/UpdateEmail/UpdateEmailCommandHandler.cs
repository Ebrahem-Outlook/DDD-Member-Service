using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdateEmail;

internal sealed class UpdateEmailCommandHandler : ICommandHandler<UpdateEmailCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<UpdateEmailCommandHandler> _logger;

    public UpdateEmailCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, ILogger<UpdateEmailCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public async Task<string> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service start...");

        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member == null)
        {
            _logger.LogError($"Member with specified Id {request.MemberId} does not exist.");

            throw new NullReferenceException();
        }

        member.UpdateEmail(request.Email);

        _memberRepository.Update(member);

        string token = _jwtProvider.GenerateToken(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Service Success...");

        return token;
    }
}
