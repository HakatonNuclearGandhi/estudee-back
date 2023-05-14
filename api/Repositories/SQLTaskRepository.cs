using System.Diagnostics;
using api.Data;
using api.Models.Domain;
using api.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Task = api.Models.Domain.Task;

namespace api.Repositories;

public class SQLTaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SQLTaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Task> CreateAsync(Task task)
    {
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();

        return task;
    }

    public async Task<Task>? GetByIdAsync(Guid id)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.taskId == id);
        return task;
    }

    public async Task<Task>? DeleteAsync(Guid id)
    {
        var exist = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.taskId == id);
        
        if (exist == null)
        {
            return null;
        }

        _dbContext.Tasks.Remove(exist);
        await _dbContext.SaveChangesAsync();

        return exist;
    }

    public async Task<List<TaskResponseDto>> GetAllAsync(User user)
    {
        var tasks = _dbContext.Tasks.AsQueryable();
        List<Subject> listSub = await _dbContext.Subjects.Where(s => s.User == user).ToListAsync();
        Debug.WriteLine("checj sosog \n\n\n");
        for (int i = 0; i < listSub.Count; i++)
        {
            Debug.WriteLine(listSub[i]);
        }
        
        List<Task> tasksList = new();
        foreach (var sub in listSub)
        {
            List<Task> tList = await _dbContext.Tasks.Where(t => t.subjectId == sub.subjectId).ToListAsync();
            tasksList.AddRange(tList);
        }

        List<TaskResponseDto> taskResponseDtos = new List<TaskResponseDto>();
        for (int i = 0; i < tasksList.Count; i++)
        {
            taskResponseDtos.Add(new TaskResponseDto()
            {
                subjectId = tasksList[i].subjectId,
                subjectName = "test_name",
                maxScore = tasksList[i].maxScore,
                taskName = tasksList[i].taskName,
                deadline = tasksList[i].deadLine,
                statusId = tasksList[i].statusId,
                statusName = "test_name2"
            });
        }
        
        Debug.WriteLine("checj sosog \n\n\n");
        for (int i = 0; i < tasksList.Count; i++)
        {
            Debug.WriteLine(tasksList[i]);
        }
        return taskResponseDtos;
    }

    public async Task<List<Task>> GetOnWeekAsync()
    {
        var tasks = _dbContext.Tasks.AsQueryable();

        return await tasks.Where(t => t.deadLine > DateTime.Now && 
                                      t.deadLine <= DateTime.Now.AddDays(7)).ToListAsync();
    }

    public async Task<Task>? UpdateAsync(Guid id, Task task)
    {
        var exist = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.taskId == id);
        
        if (exist == null)
        {
            return null;
        }

        exist.subjectId = task.subjectId;
        exist.maxScore = task.maxScore;
        exist.score = task.score;
        exist.taskName = task.taskName;
        exist.deadLine = task.deadLine;
        exist.statusId = task.statusId;

        await _dbContext.SaveChangesAsync();
        return exist;
    }
}