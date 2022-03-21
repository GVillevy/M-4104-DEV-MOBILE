using System;
using System.Collections.ObjectModel;
using pokeworld.Models;
using PokeApiNet;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace pokeworld.ViewModels
{
    class PokemonListViewModel : BaseViewModel
    {
        private static PokemonListViewModel _instance = new PokemonListViewModel();
        public static PokemonListViewModel Instance { get { return _instance; } }
        private PokemonModel myPokemon;
        private int nbType;
        public int nbrPokeDatabase = 0;

        /*
         * Création de notre liste de pokemon
         */
        public ObservableCollection<PokemonModel> PokemonsList
        {
            get { return GetValue<ObservableCollection<PokemonModel>>(); }
            set { SetValue(value); }
        }

        /*
         * Fonction principale qui instancie notre list et appelle la procédure permettant de récuperer les pokemons
         */
        public PokemonListViewModel()
        {
            PokemonsList = new ObservableCollection<PokemonModel>();
            GetPokemonList();
        }
        /*
         * Procédure permettant de récupérer notre liste de pokemon, depuis l'API
         */
        public async void GetPokemonList()
        {
            /*
             * Appelle de l'API et récupération du nombre de pokemon dans notre base de donnée
             */
            PokeApiClient pokeClient = new PokeApiClient();
            nbrPokeDatabase = await App.Database.connection.Table<PokemonModel>().CountAsync();

            /*
             * Bouble qui permet d'ajouter les pokemon dans notre base de donnée et de vérifier si ils ont bien un type.
             */
            for (var i = 0; i < nbrPokeDatabase; i++)
            {
                myPokemon = await App.Database.connection.Table<PokemonModel>().ElementAtAsync(i);
                if (!String.IsNullOrEmpty(myPokemon.Type1))
                {
                    myPokemon.TypeImg1 = GetImageByType(myPokemon.Type1);
                }
                if (!String.IsNullOrEmpty(myPokemon.Type2))
                {
                    myPokemon.TypeImg2 = GetImageByType(myPokemon.Type2);
                }
                PokemonsList.Add(myPokemon);
            }

            if (nbrPokeDatabase == 0)
            {
                /*
                 * Boucle permettant de compléter notre liste de pokemon et de les ajouter en base.
                 */
                for (int i = 1; i < 50; i++)
                {
                    /*
                     * Création d'un tâche asynchrone pour récupérer les pokemon de l'API
                     */
                    Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));
                    nbType = pokemon.Types.Count;

                    /*
                     * Création de notre pokemon et ajout de ses caractéristiques
                     */
                    myPokemon = new PokemonModel()
                    {
                        Id = pokemon.Id,
                        Name = pokemon.Name,
                        Image = pokemon.Sprites.FrontDefault,
                        Weight = pokemon.Weight / 10.0,
                        Height = pokemon.Height / 10.0,
                        BackgroundColorByType = GetBackgroundColorByType(pokemon.Types[0].Type.Name),
                        TypeImg1 = GetImageByType(pokemon.Types[0].Type.Name),
                        Type1 = pokemon.Types[0].Type.Name,
                        HP = pokemon.Stats[0].BaseStat,
                        Attack = pokemon.Stats[1].BaseStat,
                        Defense = pokemon.Stats[2].BaseStat,
                        Speed = pokemon.Stats[5].BaseStat,

                    };

                    /*
                     * Vérification si le pokemon à un deuxième type
                     */
                    if (nbType == 2)
                    {
                        myPokemon.TypeImg2 = GetImageByType(pokemon.Types[1].Type.Name);
                        myPokemon.Type2 = pokemon.Types[1].Type.Name;
                    }
                    /*
                     *On spécifie que le pokemon viens de l'API, on l'ajoute à notre base de donnée et dans la liste. 
                     */
                    myPokemon.IsFromApi = true;
                    await App.Database.connection.InsertAsync(myPokemon);
                    PokemonsList.Add(myPokemon);
                }
            }
        }
        /*
         * Fonction qui prend un string en paramètre et qui renvoie un string représentant une couleur en fonction du type
         */
        public string GetBackgroundColorByType(string Type)
        {
            switch (Type)
            {
                case "grass": return "#78C850";
                case "fire": return "#F08030";
                case "water": return "#6890F0";
                case "normal": return "#A8A878";
                case "fighting": return "#C03028";
                case "bug": return "#A8B820";
                case "flying": return "##AED6F1";
                case "poison": return "#A040A0";
                case "rock": return "#B8A038";
                case "ground": return "#E0C068";
                case "steel": return "#BFC9CA";
                case "dragon": return "#7038F8";
                case "ice": return "#98D8D8";
                case "fairy": return "#EE99AC";
                case "dark": return "#35373C";
                case "ghost": return "#705898";
                case "electric": return "#F4D03F";
                case "psychic": return "#F85888";
                default: return "#FFFFFF";
            }
        }
        /*
         * Fonction qui prend un string en paramètre et qui renvoie un string représentant une image en fonction du type
         */
        public string GetImageByType(string Type)
        {
            switch (Type)
            {
                case "grass": return "grass.png";
                case "fire": return "fire.png";
                case "water": return "water.png";
                case "normal": return "normal.png";
                case "fighting": return "fighting.png";
                case "bug": return "bug.png";
                case "flying": return "flying.png";
                case "poison": return "poison.png";
                case "rock": return "rock.png";
                case "ground": return "ground.png";
                case "steel": return "steel.png";
                case "dragon": return "dragon.png";
                case "ice": return "ice.png";
                case "fairy": return "fairy.png";
                case "dark": return "dark.png";
                case "ghost": return "ghost.png";
                case "electric": return "electric.png";
                case "psychic": return "psychic.png";
                default: return "normal.png";
            }
        }
    }
}