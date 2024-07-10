namespace Domain.Members;

public interface IMemberRepository
{
    // Commands.
    Task AddAsync(Member member, CancellationToken cancellationToken = default);
    void Update(Member member);
    void Delete(Member member);

    // Queries.
    Task<List<Member>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<Member>?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> IsEmailExest(string email, CancellationToken cancellationToken);
}
