using MediatR;

namespace PokemonApi.Features.Commands
{
    public class DeleteTeamCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
