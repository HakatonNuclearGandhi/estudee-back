using api.Data;
using api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class SQLSubjectRepository: ISubjectRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SQLSubjectRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Subject> CreateAsync(Subject subject)
    {
        await _dbContext.Subjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();

        return subject;
    }

    public async Task<List<Subject>> GetAllSubjectAsync(User user)
    {
        List<Subject> subjects = await _dbContext.Subjects.Where(s => s.User == user).ToListAsync();

        return subjects;

    }

    public async Task<Subject>? UpdateAsync(Subject subject)
    {
        var id = subject.subjectId;
        var exist = await _dbContext.Subjects.FirstOrDefaultAsync(s => s.subjectId == id);
        
        if (exist == null)
        {
            return null;
        }

        exist.name = subject.name;
        exist.description = subject.description;
        exist.maxScore = subject.maxScore;
        exist.currentScore = subject.currentScore;

        await _dbContext.SaveChangesAsync();

        return exist;

    }
}