using Domain.Core.BaseType;
using Domain.Members.Events;

namespace Domain.Members;

public sealed class Member : AggregateRoot
{
    private Member(string firstName, string lastName, string email, string password) 
        : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    private Member() : base() { }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password{ get; private set; } = default!;

    public static Member Create(string firstName, string lastName, string email, string password)
    {
        Member member = new(firstName, lastName, email, password);

        member.RaiseDomainEvent(new MemberCreatedDomainEvent(member));

        return member;
    }

    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        RaiseDomainEvent(new MemberNameUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public void UpdateEmail(string email)
    {
        Email = email;

        RaiseDomainEvent(new MemberEmailUpdatedDomainEvent(Id, Email));
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }
}
