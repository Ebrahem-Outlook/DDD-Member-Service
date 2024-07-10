using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.DeleteMember;

internal sealed class DeleteMemberCommandHandler(
    IMemberRepository memberRepository, 
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider, 
    Logger<DeleteMemberCommandHandler> logger) : ICommandHandler<DeleteMemberCommand, string>
{
    public async Task<string> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service start...");

        Member? member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            logger.LogError($"User with the specified Id {request.MemberId} does not exist.");

            throw new NullReferenceException();
        }

        memberRepository.Delete(member);

        string token = jwtProvider.GenerateToken(member);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Service start...");

        return token;
    }
}
