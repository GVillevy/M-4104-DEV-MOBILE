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
        public PokemonModel pokemonToRemove;
        public int IdPokemonToRemove;

        public DescriptionPage(PokemonModel pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
            pokemonToRemove = pokemon;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            IdPokemonToRemove = pokemonToRemove.Id;

            if (!pokemonToRemove.IsFromApi)
            {
                PokemonListViewModel.Instance.PokemonsList.Remove(pokemonToRemove);
                await App.Database.connection.DeleteAsync<PokemonModel>(IdPokemonToRemove);
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