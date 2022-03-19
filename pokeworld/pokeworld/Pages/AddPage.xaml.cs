using pokeworld.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using pokeworld.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace pokeworld.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public string selectedImagePath = "";

        public AddPage()
        {
            InitializeComponent();
        }


        private async void OnNewButtonClicked(object sender, EventArgs e)
        {
            PokemonModel pokemon = new PokemonModel
            {
                Name = nomPokemon.Text,
                Image = selectedImagePath,
                Type1 = type1Pokemon.Text,
                Type2 = type2Pokemon.Text,
                Height = Int16.Parse(heightPokemon.Text),
                Weight = Int16.Parse(weightPokemon.Text),
            };

            if (!String.IsNullOrEmpty(pokemon.Type1))
            {
                pokemon.TypeImg1 = PokemonListViewModel.Instance.getImageByType(pokemon.Type1);
            }
            if (!String.IsNullOrEmpty(pokemon.Type2))
            {
                pokemon.TypeImg2 = PokemonListViewModel.Instance.getImageByType(pokemon.Type2);
            }
            pokemon.IsFromApi = false;

            PokemonListViewModel.Instance.PokemonsList.Insert(0, pokemon);

            await App.Database.connection.InsertAsync(pokemon);
            await DisplayAlert("Succès", $"Pokémon : {pokemon.Name} ajouté !", "Ok");


            nomPokemon.Text = nomPokemon.Text = string.Empty;
            selectionImage.Source = "ajout.png";
            selectedImagePath = null;
            type1Pokemon.Text = null;
            type2Pokemon.Text = null;
            heightPokemon.Text = null;
            weightPokemon.Text = null;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Non supporté", "Cette fonctionnalité n'est pas supportée", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await DisplayAlert("Erreur", "Aucune image sélectionnée", "Ok");
                return;
            }

            selectionImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
            selectedImagePath = selectedImageFile.Path;
        }
    }
}