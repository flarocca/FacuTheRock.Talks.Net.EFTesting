using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacuTheRock.Talks.Net.EFTesting.API.Dtos;
using FacuTheRock.Talks.Net.EFTesting.Database.Exceptions;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;
using FacuTheRock.Talks.Net.EFTesting.Database.Repositories;

namespace FacuTheRock.Talks.Net.EFTesting.API.Controllers
{
    [ApiController]
    [Route("teams/{teamId}/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IMapper _mapper;

        public PlayersController(IPlayersRepository playersRepository, IMapper mapper)
        {
            _playersRepository = playersRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersAsync(Guid teamId)
        {
            var players = await _playersRepository.GetAllAsync(teamId);
            var response = _mapper.Map<IEnumerable<PlayerDto>>(players);
            return Ok(response);
        }

        [HttpGet("{id}", Name = nameof(GetPlayerAsync))]
        public async Task<ActionResult<PlayerDto>> GetPlayerAsync(Guid teamId, Guid id)
        {
            var player = await _playersRepository.GetAsync(teamId, id);

            if(player == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PlayerDto>(player);
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<ActionResult<TeamDto>> CreatePlayerAsync(Guid teamId, [FromBody] PlayerDto request)
        {
            try
            {
                var player = _mapper.Map<Player>(request);
                player.TeamId = teamId;
                var createdId = await _playersRepository.AddAsync(player);

                return CreatedAtRoute(nameof(GetPlayerAsync), new { teamId, id = createdId }, createdId);
            }
            catch (TeamNotExistsException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayerAsync(Guid teamId, Guid id)
        {
            await _playersRepository.DeleteAsync(teamId, id);
            return NoContent();
        }
    }
}