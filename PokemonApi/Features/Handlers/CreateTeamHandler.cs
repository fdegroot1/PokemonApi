using MediatR;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Features.Commands;
using PokemonApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonApi.Features.Handlers
{
    public class CreateTeamHandler : IRequestHandler<CreateTeamCommand,Team>
    {
        private readonly PokemonContext _context;
        public CreateTeamHandler(PokemonContext context)
        {
            _context = context;
        }

        public async Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team { Name = request.Name };
            foreach (var pokemonId in request.PokemonIds)
            {
                var pokemon = await _context.Pokemons.FindAsync(pokemonId);
                if (pokemon != null)
                {
                    team.Pokemons.Add(pokemon);
                }
            }
            _context.Teams.Add(team);
            await _context.SaveChangesAsync(cancellationToken);

            return team;
        }
    }
}
