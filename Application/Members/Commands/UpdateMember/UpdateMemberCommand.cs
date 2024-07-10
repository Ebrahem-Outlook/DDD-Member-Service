using Application.Core.Abstractions.Authentications;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Members;
using Microsoft.Extensions.Logging;

namespace Application.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(
    Guid MemberId, 
    string FirstName, 
    string LastName) : ICommand<string>;

internal sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<UpdateMemberCommandHandler> _logger;

    public UpdateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, ILogger<UpdateMemberCommandHandler> logger)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public Task<string> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("service start");
    }
}