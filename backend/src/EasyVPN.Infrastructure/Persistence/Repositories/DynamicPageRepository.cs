using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class DynamicPageRepository : IDynamicPageRepository
{
    private readonly EasyVpnDbContext _dbContext;

    public DynamicPageRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DynamicPage? Get(string route)
    {
        return _dbContext.DynamicPages.SingleOrDefault(p => p.Route == route);
    }

    public IEnumerable<DynamicPage> GetAll()
    {
        return _dbContext.DynamicPages;
    }

    public void Add(DynamicPage dynamicPage)
    {
        _dbContext.DynamicPages.Add(dynamicPage);
        _dbContext.SaveChanges();
    }

    public void Remove(string route)
    {
        if (_dbContext.DynamicPages.SingleOrDefault(p => p.Route == route)
            is not { } page)
            return;

        _dbContext.DynamicPages.Remove(page);
        _dbContext.SaveChanges();
    }

    public void Update(DynamicPage dynamicPage)
    {
        if (_dbContext.DynamicPages.SingleOrDefault(p => p.Route == dynamicPage.Route)
            is not { } statePage)
            return;

        statePage.Title = dynamicPage.Title;
        statePage.Content = dynamicPage.Content;
        statePage.Created = dynamicPage.Created;
        statePage.LastModified = dynamicPage.LastModified;

        _dbContext.DynamicPages.Update(statePage);
        _dbContext.SaveChanges();
    }
}