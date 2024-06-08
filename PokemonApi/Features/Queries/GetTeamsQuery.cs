using MediatR;
using PokemonApi.Models;
using System.Collections.Generic;

namespace PokemonApi.Features.Queries
{
    public class GetTeamsQuery : IRequest<IEnumerable<Team>>
    {
    }
}
