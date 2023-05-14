using AutoMapper;
using api.Models.Domain;
using api.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task = api.Models.Domain.Task;

namespace api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Status, StatusDto>().ReverseMap();
        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<AddSubjectDto, Subject>().ReverseMap();
        CreateMap<Subject, GetSubjectDto>().ReverseMap();
        CreateMap<Subject, GetSubjectNameDto>().ReverseMap();
        CreateMap<Subject, UpdateSubjectDto>().ReverseMap();
        CreateMap<Task, TaskResponseDto>().ReverseMap();
        CreateMap<Status, CreateStatusDto>().ReverseMap();
    }
}