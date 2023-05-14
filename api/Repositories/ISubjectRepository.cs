using api.Models.Domain;

namespace api.Repositories;

public interface ISubjectRepository
{
    Task<Subject> CreateAsync(Subject subject);

    Task<List<Subject>> GetAllSubjectAsync(User user);

    Task<Subject>? UpdateAsync(Subject subject);
}