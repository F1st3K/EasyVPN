using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Infrastructure.Persistence.Repositories;

public class DynamicPageRepository : IDynamicPageRepository
{
    private readonly EasyZsvDbContext _dbContext;

    public DynamicPageRepository(EasyZsvDbContext dbContext)
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
        _dbContext.DynamicPages.Remove(
            _dbContext.DynamicPages.Single(p => p.Route == route));
        _dbContext.SaveChanges();
    }

    public void Update(DynamicPage dynamicPage)
    {
        _dbContext.DynamicPages.Update(dynamicPage);
        _dbContext.SaveChanges();
    }
}