using PokeApiNet;
using pokeworld.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace pokeworld.ViewModels
{
    public class ResearchPokemonViewModel : BaseViewModel
    {
        private static ResearchPokemonViewModel _instance = new ResearchPokemonViewModel();
        public static ResearchPokemonViewModel Instance { get { return _instance; } }
        private PokemonModel myPokemon;
        public Command SearchPokemon { get; }
        public string PokemonSearched
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<PokemonModel> PokemonReasarched
        {
            get { return GetValue<ObservableCollection<PokemonModel>>(); }
            set { SetValue(value); }
        }

        public ResearchPokemonViewModel()
        {
            PokemonReasarched = new ObservableCollection<PokemonModel>();
            SearchPokemon = new Command(SearchPokemonAction);
        }
      
        private void SearchPokemonAction()
        {
            SearchPokemonByName();
        }

        private async void SearchPokemonByName()
        {
            PokemonReasarched.Clear();
            String pokemonName = PokemonSearched.Trim().ToLower();

            try
            {
                PokeApiClient pokeClient = new PokeApiClient();
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(pokemonName));
                int nbType = pokemon.Types.Count;

                myPokemon = new PokemonModel()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Image = pokemon.Sprites.FrontDefault,
                    Weight = pokemon.Weight,
                    Height = pokemon.Height,
                    TypeImg1 = PokemonListViewModel.Instance.GetImageByType(pokemon.Types[0].Type.Name),
                    Type1 = pokemon.Types[0].Type.Name,
                    BackgroundColorByType = PokemonListViewModel.Instance.GetBackgroundColorByType(pokemon.Types[0].Type.Name),
                    HP = pokemon.Stats[0].BaseStat,
                    Attack = pokemon.Stats[1].BaseStat,
                    Defense = pokemon.Stats[2].BaseStat,
                    Speed = pokemon.Stats[5].BaseStat,

                };

                if (nbType == 2)
                {
                    myPokemon.TypeImg2 = PokemonListViewModel.Instance.GetImageByType(pokemon.Types[1].Type.Name);
                    myPokemon.Type2 = pokemon.Types[1].Type.Name;
                }
                PokemonReasarched.Add(myPokemon);
            }
            

            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Erreur", "Le pokemon recherché n'a pas été trouvé !", "Réessayez");
                Console.WriteLine("Aucun pokemon trouvé " + e.Message);
            }
        }
    }
}