using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.Common.Interfaces.Persistence;

public interface IDynamicPageRepository
{
    public DynamicPage? Get(string route);
    public IEnumerable<DynamicPage> GetAll();
    public void Add(DynamicPage dynamicPage);
    public void Remove(string route);
    public void Update(DynamicPage dynamicPage);
}