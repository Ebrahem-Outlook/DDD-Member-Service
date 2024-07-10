namespace Domain.Members;

public interface IMemberRepository
{
    // Commands.
    Task AddAsync(Member member, CancellationToken cancellationToken = default);
    void Update(Member member, CancellationToken cancellationToken = default);

    // Queries.
    Task<IEnumerable<Member>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Member>?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
