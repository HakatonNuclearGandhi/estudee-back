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

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public DashboardController(IMapper mapper, IDashboardRepository dashboardRepository, IConfiguration configuration, UserManager<User> userManager)
    {
        _dashboardRepository = dashboardRepository;
        _configuration = configuration;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    //[Authorize]
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
        
        var statusesDom = await _dashboardRepository.GetAsync(user);
        var statusesDto = _mapper.Map<List<StatusDto>>(statusesDom);
        return Ok(statusesDto);
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