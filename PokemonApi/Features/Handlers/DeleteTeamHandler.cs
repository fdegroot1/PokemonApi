using MediatR;
using PokemonApi.Data;
using PokemonApi.Features.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonApi.Features.Handlers
{
    public class DeleteTeamHandler : IRequestHandler<DeleteTeamCommand, Unit>
    {
        private readonly PokemonContext _context;

        public DeleteTeamHandler(PokemonContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(request.Id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
