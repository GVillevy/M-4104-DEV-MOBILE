using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using pokeworld.Models;
using SQLite;

namespace pokeworld
{
    public class Database
    {
        public readonly SQLiteAsyncConnection connection;

        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<PokemonModel>();
        }
    }
}