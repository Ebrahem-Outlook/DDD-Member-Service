using Domain.Members;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Caching;

internal sealed class CachedMemberRepository(MemberRepository decorated, IMemoryCache memoryCache) : IMemberRepository
{
    public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        await decorated.AddAsync(member, cancellationToken);
        string key = $"Key-{member.Email}";
        memoryCache.Remove(key);
    }

    public void Update(Member member)
    {
        decorated.Update(member);
        string key = $"Key-{member.Email}";
        memoryCache.Remove(key);
    }

    public void Delete(Member member)
    {
        decorated.Delete(member);
        string key = $"Key-{member.Email}";
        memoryCache.Remove(key);
    }

    public async Task<List<Member>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        string key = $"Key-AllMembers";
        return await memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return decorated.GetAllAsync(cancellationToken);
        });
    }

    public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        string key = $"Key-{email}";
        return await memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return decorated.GetByEmailAsync(email, cancellationToken);
        });
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string key = $"Key-{id}";
        return await memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return decorated.GetByIdAsync(id, cancellationToken);
        });
    }

    public async Task<List<Member>?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        string key = $"Key-Names";
        return await memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return decorated.GetByNameAsync(name, cancellationToken);
        });
    }

    public async Task<bool> IsEmailExest(string email, CancellationToken cancellationToken)
    {
        string key = $"Key-{email}";
        return await memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return decorated.IsEmailExest(email, cancellationToken);
        });
    }
}
