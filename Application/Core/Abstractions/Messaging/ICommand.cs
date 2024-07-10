using MediatR;

namespace Application.Core.Abstractions.Messaging;

public interface ICommand<TResponse> : IRequest<TResponse>
{

}
