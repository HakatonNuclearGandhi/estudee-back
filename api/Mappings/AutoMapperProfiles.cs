using AutoMapper;
using api.Models.Domain;
using api.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Status, StatusDto>().ReverseMap();
        CreateMap<Models.Domain.Task, TaskDto>().ReverseMap();
        CreateMap<AddSubjectDto, Subject>().ReverseMap();
        CreateMap<Subject, GetSubjectDto>().ReverseMap();
        CreateMap<Subject, GetSubjectNameDto>().ReverseMap();
        CreateMap<Subject, UpdateSubjectDto>().ReverseMap();
    }
}