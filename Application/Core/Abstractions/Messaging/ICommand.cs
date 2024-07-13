using MediatR;

namespace Application.Core.Abstractions.Messaging;

public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
    where TResponse : class
{

}

public interface ICommand : IRequest 
{
    
}

public interface ICommandBase
{

}
