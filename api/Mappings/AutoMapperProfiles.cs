using AutoMapper;
using api.Models.Domain;
using api.Models.DTO;

namespace api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Status, StatusDto>();
    }
}