using Application.Core.Abstractions.Data;
using Domain.Members;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class MemberRepository(IDbContext dbContext) : IMemberRepository
{
    public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<Member>().AddAsync(member, cancellationToken);
    }

    public void Update(Member member)
    {
        dbContext.Set<Member>().Update(member);
    }

    public void Delete(Member member)
    {
        dbContext.Set<Member>().Remove(member);
    }

    public async Task<IEnumerable<Member>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Member>().ToListAsync(cancellationToken);
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Member>().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Member>().FirstOrDefaultAsync(m => m.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<Member>?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Member>().Where(m => m.FirstName == name).ToListAsync(cancellationToken);
    }
}
