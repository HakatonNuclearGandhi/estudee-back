using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Models.Domain;
using api.Models.DTO;
using api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IConfiguration _configuration;

        public SubjectController(UserManager<User> userManager, IMapper mapper, ISubjectRepository subjectRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _configuration = configuration;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] AddSubjectDto model)
        {
            string? accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
            ;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);
            
            var subject = _mapper.Map<Subject>(model);
            subject.User = user;
            await _subjectRepository.CreateAsync(subject);

            return Ok(subject);
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllSubject()
        {
            string? accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);

            var subjects = await _subjectRepository.GetAllSubjectAsync(user);
            var subjectDto = _mapper.Map<List<GetSubjectDto>>(subjects);
            return Ok(subjectDto);
        }
        
        [Authorize]
        [HttpGet]
        [Route("Name")]
        public async Task<IActionResult> GetAllSubjectName()
        {
            string? accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);

            var subjects = await _subjectRepository.GetAllSubjectAsync(user);
            var subjectNameDto = _mapper.Map<List<GetSubjectNameDto>>(subjects);
            return Ok(subjectNameDto);
        }
        
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateSubject(UpdateSubjectDto model)
        {
            var subject = _mapper.Map<Subject>(model);

            subject = await _subjectRepository.UpdateAsync(subject);

            if (subject == null)
            {
                return NotFound();
            }

            var subjectDto = _mapper.Map<GetSubjectDto>(subject);

            return Ok(subjectDto);
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
}