using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.CreateMember;

internal sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<CreateMemberCommandHandler> _logger;
    private readonly IMemberIdentifierProvider _memberIdentifier;

    public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, ILogger<CreateMemberCommandHandler> logger, IMemberIdentifierProvider memberIdentifier)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _logger = logger;
        _memberIdentifier = memberIdentifier;
    }

    public async Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service start...");

        bool IsEmailExist = await _memberRepository.IsEmailExest(request.Email, cancellationToken);

        if (IsEmailExist)
        {
            _logger.LogError("User with spicified email already exist.");

            throw new InvalidOperationException();
        }

        Member member = Member.Create(request.FirstName, request.LastName, request.Email, request.Password);

        await _memberRepository.AddAsync(member);

        string token = _jwtProvider.GenerateToken(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Service Success...");

        return token;
    }
}
