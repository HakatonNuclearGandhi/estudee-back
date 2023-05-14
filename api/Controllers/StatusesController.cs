using api.Data;
using api.Models.Domain;
using api.Models.DTO;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController : ControllerBase
{
    private readonly IStatusRepository _statusRepository;
    private readonly IMapper _mapper;
    
    public StatusesController(IMapper mapper, IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var statusesDom = await _statusRepository.GetAllAsync();
        var statusesDto = _mapper.Map<List<StatusDto>>(statusesDom);
        return Ok(statusesDto);
    }
}