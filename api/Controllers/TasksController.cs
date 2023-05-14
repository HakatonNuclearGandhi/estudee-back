using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models.Domain;
using api.Models.DTO;
using api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Task = api.Models.Domain.Task;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public TasksController(IMapper mapper, ITaskRepository taskRepository, IConfiguration configuration, UserManager<User> userManager)
    {
        _taskRepository = taskRepository;
        _configuration = configuration;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        string? accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        string username = principal.Identity.Name;
        User user = await _userManager.FindByNameAsync(username);
        
        var tasksDom = await _taskRepository.GetAllAsync(user);
        // var tasksDto = _mapper.Map<List<TaskResponseDto>>(tasksDom);
        
        return Ok(tasksDom);
    }
    
    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskDto addTaskDto)
    {
        var taskDom = _mapper.Map<Task>(addTaskDto);
        await _taskRepository.CreateAsync(taskDom);

        var taskDto = _mapper.Map<TaskDto>(taskDom);
        return Ok(taskDto);
    }
    
    //[Authorize]
    [HttpGet]
    [Route("OnWeek")]
    public async Task<IActionResult> GetAllOnWeekAsync()
    {
        string? accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        string username = principal.Identity.Name;
        User user = await _userManager.FindByNameAsync(username);
        
        var tasksDom = await _taskRepository.GetOnWeekAsync(user);
        var tasksDto = _mapper.Map<List<TaskDto>>(tasksDom);
        return Ok(tasksDto);
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal =
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}