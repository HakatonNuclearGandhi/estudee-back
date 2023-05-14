using api.Data;
using api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Task = api.Models.Domain.Task;

namespace api.Repositories;

public class SQLDashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SQLDashboardRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> GetAsync(User user)
    {
        var tasks = _dbContext.Tasks.AsQueryable();
        int subjectsSumScore = 0;
        int tasksSumScore = 0;
        List<Subject> listSub = await _dbContext.Subjects.Where(s => s.User == user).ToListAsync();
        foreach (var sub in listSub)
        {
            subjectsSumScore += sub.maxScore;
            List<Task> tList = await _dbContext.Tasks.Where(t => t.statusId == sub.subjectId).ToListAsync();
            foreach (var task in tList)
            {
                tasksSumScore += task.score;
            }
        }
        if(subjectsSumScore != 0)
            return tasksSumScore / subjectsSumScore * 100;
        return 100;
    }
}