using Application.Core.Abstractions.Authentication;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.CreateMember;

internal sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, string>
{
    private readonly ILogger<CreateMemberCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public CreateMemberCommandHandler(ILogger<CreateMemberCommandHandler> logger, IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _logger = logger;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        bool IsExist = await _memberRepository.IsEmailExest(request.Email, cancellationToken);

        if (IsExist)
        {
            throw new InvalidOperationException("The Email already exist");
        }

        Member member = Member.Create(request.FirstName, request.LastName, request.Email, request.Password);

        await _memberRepository.AddAsync(member, cancellationToken);

        string token = _jwtProvider.GenerateToken(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return token;
    }
}
