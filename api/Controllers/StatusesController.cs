using api.Data;
using api.Models.Domain;
using api.Models.DTO;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var statusesDom = await _statusRepository.GetAllAsync();
        var statusesDto = _mapper.Map<List<StatusDto>>(statusesDom);
        return Ok(statusesDto);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStatusDto model)
    {
        var status = _mapper.Map<Status>(model);
        var statusesDom = await _statusRepository.GetAllAsync();
        var statusesDto = _mapper.Map<List<StatusDto>>(statusesDom);
        return Ok(statusesDto);
    }
}