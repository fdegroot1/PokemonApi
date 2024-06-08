using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.Features.Commands;
using PokemonApi.Features.Queries;
using PokemonApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _mediator.Send(new GetTeamsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(CreateTeamCommand command)
        {
            var team = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTeams), new { id = team.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _mediator.Send(new DeleteTeamCommand { Id = id });
            return NoContent();
        }
    }
}
