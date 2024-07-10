using Domain.Members;

namespace Application.Core.Abstractions.Authentications;

public interface IJwtProvider
{
    string GenerateToken(Member member);
}
