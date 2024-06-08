using MediatR;
using PokemonApi.Models;
using System.Collections.Generic;

namespace PokemonApi.Features.Commands
{
    public class CreateTeamCommand : IRequest<Team>
    {
        public string Name { get; set; }
        public List<int> PokemonIds { get; set; }
    }
}
