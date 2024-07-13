using Domain.Core.BaseType;
using Domain.Members.Events;
using Domain.Members.ValueObjects;

namespace Domain.Members;

public sealed class Member : AggregateRoot
{
    private Member() : base() { }

    private Member(FirstName firstName, LastName lastName, Email email, Password password) 
        : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public FirstName FirstName { get; private set; } = default!;
    public LastName LastName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public Password Password { get; private set; } = default!;

    public static Member Create(FirstName firstName, LastName lastName, Email email, Password password)
    {
        Member member = new(firstName, lastName, email, password);

        member.RaiseDomainEvent(new MemberCreatedDomainEvent(member));

        return member;
    }

    public void UpdateName(FirstName firstName, LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        RaiseDomainEvent(new MemberNameUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public void UpdateEmail(Email email)
    {
        Email = email;

        RaiseDomainEvent(new MemberEmailUpdatedDomainEvent(Id, Email));
    }

    public void UpdatePassword(Password password)
    {
        Password = password;

        RaiseDomainEvent(new MemberPasswordUpdatedDomainEvent(Id, Password));
    }
}
