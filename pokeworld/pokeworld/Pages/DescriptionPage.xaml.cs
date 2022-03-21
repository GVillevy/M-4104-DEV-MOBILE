using pokeworld.Models;
using pokeworld.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeworld.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {
        public PokemonModel pokemon;
        public int pokemonId;

        /*
         * Constructeur de la classe DescriptionPage, prenant en paramètre 
         * notre pokémon sur lequel on clique dans notre méthode OnClick de la classe ListPage
         */
        public DescriptionPage(PokemonModel pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
            this.pokemon = pokemon;
        }

        /*
         * Fonction apellée au clique du bouton supprimer, permettant de supprimer un pokémon
         */
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            pokemonId = pokemon.Id;

            /*
             * On vérifie que notre pokémon ne provient pas de l'API
             */
            if (!pokemon.IsFromApi)
            {
                /*
                 * On supprime le pokémon
                 */
                PokemonListViewModel.Instance.PokemonsList.Remove(pokemon);
                await App.Database.connection.DeleteAsync<PokemonModel>(pokemonId);
                await Navigation.PushAsync(new ListPage());
            }
            else
            {
                await DisplayAlert("Erreur", "Impossible de supprimer un pokémon classique", "Ok");
                return;
            }
        }
    }
}