using EasyVPN.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<
        (Guid id, Type typeRequest),
        (DateTime execTime, IBaseRequest request, Type typeResponse)> _tasks = new ();
    
    public void PushTask<TResponse>(Guid taskId, DateTime execTime, IRequest<ErrorOr<TResponse>> request)
    {
        _tasks[(taskId, request.GetType())] = (execTime, request, typeof(TResponse));
    }

    public TRequest? PopTask<TRequest>(Guid taskId)
    {
        var task = _tasks[(taskId, typeof(TRequest))]; 
        _tasks.Remove((taskId, typeof(TRequest)));
        return (TRequest) task.request;
    }
    
    public void RemoveTaskIfExist(Guid taskId, Type requestType)
    {
        _tasks.Remove((taskId, requestType));
    }

    public IReadOnlyDictionary<
        (Guid id, Type typeRequest),
        (DateTime execTime, IBaseRequest request, Type typeResponse)> GetTasks()
    {
        return _tasks.ToDictionary(
            x => x.Key, 
            x => 
                (x.Value.execTime, x.Value.request, x.Value.typeResponse));
    }
}