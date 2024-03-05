namespace EasyVPN.Application.Common.Interfaces.Expire;

public interface IExpireService<in T>
{
    public void AddAllToTrackExpire();
    public void AddTrackExpire(T entity);
}