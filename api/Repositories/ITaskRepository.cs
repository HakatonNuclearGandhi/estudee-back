using api.Models.Domain;
using api.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task = api.Models.Domain.Task;

namespace api.Repositories;

public interface ITaskRepository
{
    Task<Models.Domain.Task> CreateAsync(Models.Domain.Task task);

    Task<Models.Domain.Task>? GetByIdAsync(Guid id);

    Task<Models.Domain.Task>? DeleteAsync(Guid id);

    Task<List<TaskResponseDto>> GetAllAsync(User user);
    
    Task<List<Models.Domain.Task>> GetOnWeekAsync();

    Task<Models.Domain.Task>? UpdateAsync(Guid id, Models.Domain.Task task);
}