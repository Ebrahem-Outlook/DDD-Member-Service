using MediatR;

namespace Application.Core.Abstractions.Messaging;

public interface ICommmandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{

}
