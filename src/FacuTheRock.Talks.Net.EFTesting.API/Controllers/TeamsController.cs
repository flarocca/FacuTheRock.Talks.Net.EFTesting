using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.API.Dtos;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;
using FacuTheRock.Talks.Net.EFTesting.Database.Repositories;

namespace FacuTheRock.Talks.Net.EFTesting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly IMapper _mapper;

        public TeamsController(ITeamsRepository teamsRepository, IMapper mapper)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsAsync()
        {
            var teams = await _teamsRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<TeamDto>>(teams);
            return Ok(response);
        }

        [HttpGet("{id}", Name = nameof(GetTeamAsync))]
        public async Task<ActionResult<TeamDto>> GetTeamAsync(Guid id)
        {
            var team = await _teamsRepository.GetAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<TeamDto>(team);
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<ActionResult<TeamDto>> CreateTeamAsync([FromBody] TeamDto request)
        {
            var team = _mapper.Map<Team>(request);
            var createdId = await _teamsRepository.AddAsync(team);

            return CreatedAtRoute(nameof(GetTeamAsync), new { id = createdId }, createdId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeamAsync(Guid id)
        {
            await _teamsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}