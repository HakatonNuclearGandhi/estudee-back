using api.Models.Domain;

namespace api.Repositories;

public interface IDashboardRepository
{
    Task<int> GetAsync(User user);
}