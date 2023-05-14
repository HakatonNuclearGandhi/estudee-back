using api.Data;
using api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class SQLStatusRepository : IStatusRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SQLStatusRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Status>> GetAllAsync()
    {
        var statuses = _dbContext.Status.AsQueryable();

        return await statuses.ToListAsync();
    }
}