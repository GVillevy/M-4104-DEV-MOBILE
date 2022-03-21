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
        /*
         * Création de la commande pour faire une recherche de pokemon
         */
        public Command SearchPokemon { get; }
        public string PokemonSearched
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        /*
         * Liste observable pour stocker le pokemon trouver
         */
        public ObservableCollection<PokemonModel> PokemonReasarched
        {
            get { return GetValue<ObservableCollection<PokemonModel>>(); }
            set { SetValue(value); }
        }

        public ResearchPokemonViewModel()
        {
            /*
             * Constructeur qui initialise la liste et la commande
             */
            PokemonReasarched = new ObservableCollection<PokemonModel>();
            SearchPokemon = new Command(SearchPokemonAction);
        }

        /*
         * La commande appelle notre procédure asynchrone 
         */
        private void SearchPokemonAction()
        {
            SearchPokemonByName();
        }
        /*
         * Procédure de recherche de pokemon, qui renvoie un pokemon en fonction du nom tappé dans une searchBar
         */
        private async void SearchPokemonByName()
        {
            /*
             * On vide la liste de pokemon rechercher, pour en afficher toujours un seul et on convertit les caractères en minuscules pour pas avoir de conflit.
             */
            PokemonReasarched.Clear();
            String pokemonName = PokemonSearched.Trim().ToLower();

            try
            {
                /*
                 * On appelle l'API pokemon, on lance une tâche asynchrone pour récuperer un pokemon en fonction du nom  
                 */
                PokeApiClient pokeClient = new PokeApiClient();
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(pokemonName));
                int nbType = pokemon.Types.Count;

                /*
                 * Création d'un nouveau pokémon et ajout de toute ses caractéristiques
                 */
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

                /*
                 * On vérif si le pokemon en question a plusieurs type pour l'ajouter ou non.
                 */
                if (nbType == 2)
                {
                    myPokemon.TypeImg2 = PokemonListViewModel.Instance.GetImageByType(pokemon.Types[1].Type.Name);
                    myPokemon.Type2 = pokemon.Types[1].Type.Name;
                }
                PokemonReasarched.Add(myPokemon);
            }


            catch (Exception e)
            {
                /*
                 * Si le nom ne correspond a aucun pokemon, alors on alert l'utilisateur
                 */
                await App.Current.MainPage.DisplayAlert("Erreur", "Le pokemon recherché n'a pas été trouvé !", "Réessayez");
                //Console.WriteLine("Aucun pokemon trouvé " + e.Message);
            }
        }
    }
}