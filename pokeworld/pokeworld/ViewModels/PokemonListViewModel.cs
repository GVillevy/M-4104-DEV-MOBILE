using System;
using System.Collections.ObjectModel;
using pokeworld.Models;
using System.Collections.Generic;
using System.ComponentModel;
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

        public ObservableCollection<PokemonModel> PokemonsList
        {
            get { return GetValue<ObservableCollection<PokemonModel>>(); }
            set { SetValue(value); }
        }
        public PokemonListViewModel()
        {
            PokemonsList = new ObservableCollection<PokemonModel>();
            GetPokemonList();
        }
        public async void GetPokemonList()
        {

            PokeApiClient pokeClient = new PokeApiClient();
            nbrPokeDatabase = await App.Database.connection.Table<PokemonModel>().CountAsync();

            for (var i = 0; i < nbrPokeDatabase; i++)
            {
                myPokemon = await App.Database.connection.Table<PokemonModel>().ElementAtAsync(i);
                if (!String.IsNullOrEmpty(myPokemon.Type1))
                {
                    myPokemon.TypeImg1 = getImageByType(myPokemon.Type1);
                }
                if (!String.IsNullOrEmpty(myPokemon.Type2))
                {
                    myPokemon.TypeImg2 = getImageByType(myPokemon.Type2);
                }
                PokemonsList.Add(myPokemon);
            }

            for (int i = 1; i < 15; i++)
            {
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));
                nbType = pokemon.Types.Count;

                myPokemon = new PokemonModel()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Image = pokemon.Sprites.FrontDefault,
                    Weight = pokemon.Weight,
                    Height = pokemon.Height,
                    BackgroundColorByType = getBackgroundColorByType(pokemon.Types[0].Type.Name),
                    TypeImg1 = getImageByType(pokemon.Types[0].Type.Name),
                    Type1 = pokemon.Types[0].Type.Name
                };

                if (nbType == 2)
                {
                    myPokemon.TypeImg2 = getImageByType(pokemon.Types[1].Type.Name);
                    myPokemon.Type2 = pokemon.Types[1].Type.Name;
                }
                  
                
                PokemonsList.Add(myPokemon);
            }
        }
        public string getBackgroundColorByType(string Type)
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
        public string getImageByType(string Type)
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

