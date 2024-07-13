using Application.Core.Abstractins.Authentication;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdateEmail;

internal sealed class UpdateEmailCommandHandler(
    IMemberRepository memberRepository, 
    IJwtProvider jwtProvider,
    ILogger<UpdateEmailCommandHandler> logger) : ICommandHandler<UpdateEmailCommand, string>
{
    public async Task<string> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service start...");

        Member? member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member == null)
        {
            logger.LogError($"Member with specified Id {request.MemberId} does not exist.");

            throw new NullReferenceException();
        }

        member.UpdateEmail(request.Email);

        memberRepository.Update(member);

        string token = jwtProvider.GenerateToken(member);

        logger.LogInformation("Service Success...");

        return token;
    }
}
