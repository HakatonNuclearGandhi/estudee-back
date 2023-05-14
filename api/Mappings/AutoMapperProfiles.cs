using AutoMapper;
using api.Models.Domain;
using api.Models.DTO;

namespace api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Status, StatusDto>();
        CreateMap<AddSubjectDto, Subject>().ReverseMap();
        CreateMap<Subject, GetSubjectDto>().ReverseMap();
        CreateMap<Subject, GetSubjectNameDto>().ReverseMap();
        CreateMap<Subject, UpdateSubjectDto>().ReverseMap();
    }
}