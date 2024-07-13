using Application.Core.Abstractins.Authentication;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdateMember;

internal sealed class UpdateMemberCommandHandler(
    IMemberRepository memberRepository,
    IJwtProvider jwtProvider, 
    ILogger<UpdateMemberCommandHandler> logger) : ICommandHandler<UpdateMemberCommand, string>
{
    public async Task<string> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service start...");

        Member? member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            logger.LogError("User with spicified Id already exist.");

            throw new InvalidOperationException();
        }

        member.UpdateName(request.FirstName, request.LastName);

        memberRepository.Update(member);

        string token = jwtProvider.GenerateToken(member);

        logger.LogInformation("Service Success...");

        return token;
    }
}
