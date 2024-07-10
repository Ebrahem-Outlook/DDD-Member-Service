using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<UpdatePasswordCommandHandler> _logger;

    public UpdatePasswordCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, ILogger<UpdatePasswordCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service start... ");

        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            _logger.LogError("Member with spicifed Id does not exist.");

            throw new NullReferenceException();
        }

        member.UpdatePassword(request.Password);

        _memberRepository.Update(member);

        string token = _jwtProvider.GenerateToken(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Service Success... ");

        return token;
    }
}
