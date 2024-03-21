using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace EasyVPN.Infrastructure.Persistence.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private static readonly List<UserRole> _userRoles = new()
    {
        new UserRole()
        {
            Id = Guid.NewGuid(), 
            UserId = Guid.Parse("00000001-0000-0000-0000-000000000000"),
            Type = RoleType.Client
        },
        new UserRole()
        {
            Id = Guid.NewGuid(), 
            UserId = Guid.Parse("00000002-0000-0000-0000-000000000000"),
            Type = RoleType.PaymentReviewer
        },
        new UserRole()
        {
            Id = Guid.NewGuid(), 
            UserId = Guid.Parse("00000003-0000-0000-0000-000000000000"),
            Type = RoleType.Administrator
        },
    };
    
    private readonly EasyVpnDbContext _dbContext;

    public UserRoleRepository(EasyVpnDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<RoleType> GetRolesByUserId(Guid userId)
    {
        // TODO: remove this later
        if (_userRoles.Where(r => r.UserId == userId).IsNullOrEmpty() == false)
            return _userRoles.Where(r => r.UserId == userId)
                .Select(r => r.Type);
        
        return _dbContext.UserRoles.Where(r => r.UserId == userId)
            .Select(r => r.Type);
    }

    public void Add(UserRole userRole)
    {
        _dbContext.UserRoles.Add(userRole);
        _dbContext.SaveChanges();
    }
}