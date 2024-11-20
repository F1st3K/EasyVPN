using MediatR;
using ErrorOr;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface ITaskRepository
{
    void AddTask<TResponse>(Guid taskId, DateTime execTime, IRequest<ErrorOr<TResponse>> request);
    void RemoveTask(Guid taskId);
    IEnumerable<(Guid id, DateTime execTime, IBaseRequest request)> GetTasks();
}