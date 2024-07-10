using Application.Core.Abstractins.Authentication;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.CreateMember;

internal sealed class CreateMemberCommandHandler(
    IMemberRepository memberRepository, 
    IUnitOfWork unitOfWork, 
    IJwtProvider jwtProvider, 
    ILogger<CreateMemberCommandHandler> logger) : ICommandHandler<CreateMemberCommand, string>
{
    public async Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Service start...");

        bool IsEmailExist = await memberRepository.IsEmailExest(request.Email, cancellationToken);

        if (IsEmailExist)
        {
            logger.LogError("User with spicified email already exist.");

            throw new InvalidOperationException();
        }

        Member member = Member.Create(request.FirstName, request.LastName, request.Email, request.Password);

        await memberRepository.AddAsync(member);

        string token = jwtProvider.GenerateToken(member);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Service Success...");

        return token;
    }
}
