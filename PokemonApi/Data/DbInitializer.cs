using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PokemonApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonApi.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(PokemonContext context)
        {
            context.Database.EnsureCreated();

            if (await context.Pokemons.AnyAsync()) return;

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon?limit=151");
            var pokemons = JsonConvert.DeserializeObject<PokemonApiResponse>(response).Results;

            var pokemonList = new List<Pokemon>();
            foreach (var pokemon in pokemons) {
                var pokemonDetailsResponse = await httpClient.GetStringAsync(pokemon.Url);
                var pokemonDetails = JsonConvert.DeserializeObject<PokemonDetails>(pokemonDetailsResponse);
                pokemonList.Add(new Pokemon
                {
                    Id = pokemonDetails.Id,
                    Name = pokemonDetails.Name,
                    Moves = pokemonDetails.Moves.Take(4).Select(m => m.Move.Name).ToArray()
                });
            }

            await context.Pokemons.AddRangeAsync(pokemonList);
            await context.SaveChangesAsync();
        }
    }

    class PokemonApiResponse
    {
        public List<PokemonItem> Results { get; set; }
    }

    class PokemonItem
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

    class PokemonDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PokemonMove> Moves { get; set; }
    }

    class PokemonMove
    {
        public Move Move { get; set; }
    }

    class Move
    {
        public string Name { get; set; }
    }


}


