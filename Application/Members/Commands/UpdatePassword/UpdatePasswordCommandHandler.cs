using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommandHandler(
    IMemberRepository memberRepository, 
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider, 
    ILogger<UpdatePasswordCommandHandler> logger) : ICommandHandler<UpdatePasswordCommand, string>
{
    public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service start... ");

        Member? member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            logger.LogError("Member with spicifed Id does not exist.");

            throw new NullReferenceException();
        }

        member.UpdatePassword(request.Password);

        memberRepository.Update(member);

        string token = jwtProvider.GenerateToken(member);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Service Success... ");

        return token;
    }
}
