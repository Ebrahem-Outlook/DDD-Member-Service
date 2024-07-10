using Domain.Members;

namespace Application.Core.Abstractions.Authentication;

public interface IJwtProvider
{
    string CenerateToke(Member member);
}
