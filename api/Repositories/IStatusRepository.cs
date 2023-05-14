using api.Models.Domain;

namespace api.Repositories;

public interface IStatusRepository
{
    Task<List<Status>> GetAllAsync();
}