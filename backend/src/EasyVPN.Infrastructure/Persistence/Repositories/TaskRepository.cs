using EasyVPN.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<Guid, (DateTime execTime, IBaseRequest request)> _tasks = new ();
    
    public void AddTask<TResponse>(Guid taskId, DateTime execTime, IRequest<ErrorOr<TResponse>> request)
    {
        _tasks[taskId] = (execTime, request);
    }

    public void RemoveTask(Guid taskId)
    {
        _tasks.Remove(taskId);
    }

    public IEnumerable<(Guid id, DateTime execTime, IBaseRequest request)> GetTasks()
    {
        return _tasks.Select(pair => (pair.Key, pair.Value.execTime, pair.Value.request));
    }
}