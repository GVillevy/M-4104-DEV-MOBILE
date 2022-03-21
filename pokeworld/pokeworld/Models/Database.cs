using pokeworld.Models;
using SQLite;

namespace pokeworld
{
    /*
     * Class représentant notre base de donnée
     */
    public class Database
    {
        public readonly SQLiteAsyncConnection connection;

        /*
         * Création de la base de donnée et de son chemin d'accès
         */
        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<PokemonModel>();
        }
    }
}