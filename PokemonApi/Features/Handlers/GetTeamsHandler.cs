using MediatR;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Features.Queries;
using PokemonApi.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonApi.Features.Handlers
{
    public class GetTeamsHandler : IRequestHandler<GetTeamsQuery, IEnumerable<Team>>
    {
        private readonly PokemonContext _context;

        public GetTeamsHandler(PokemonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Teams.Include( t=> t.Pokemons).ToListAsync(cancellationToken);
        }
    }
}
