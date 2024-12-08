using MediatR;
using ErrorOr;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface ITaskRepository
{
    void PushTask<TResponse>(Guid taskId, DateTime execTime, IRequest<ErrorOr<TResponse>> request);
    public TRequest? TryPopTask<TRequest>(Guid taskId);
    void RemoveTaskIfExist(Guid taskId, Type responseType);

    public IReadOnlyDictionary<
        (Guid id, Type typeRequest),
        (DateTime execTime, IBaseRequest request, Type typeResponse)> GetTasks();
}