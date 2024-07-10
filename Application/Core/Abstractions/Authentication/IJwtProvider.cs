using Domain.Members;

namespace Application.Core.Abstractins.Authentication;

public interface IJwtProvider
{
    string GenerateToken(Member member);
}
