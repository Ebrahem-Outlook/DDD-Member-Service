using Application.Core.Abstractions.Messaging;
using Domain.Members;

namespace Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
    string FirstName, 
    string LastName, 
    string Email,
    string Password) : ICommand<string>;

internal sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    
    public Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
