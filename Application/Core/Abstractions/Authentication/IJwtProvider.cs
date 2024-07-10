using Domain.Members;

namespace Application.Core.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateToken(Member member);
}
