using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;

namespace PokemonApi.Data
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options) { }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }

    }
}
