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
            BindingContext = new PickerViewModel();
        }


        private async void OnNewButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nomPokemon.Text) && !string.IsNullOrWhiteSpace(selectedImagePath) && type1Pokemon.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(heightPokemon.Text) && !string.IsNullOrWhiteSpace(weightPokemon.Text))
            {
                PokemonModel pokemon = new PokemonModel
                {
                    Name = nomPokemon.Text,
                    Image = selectedImagePath,
                    Type1 = type1Pokemon.Items[type1Pokemon.SelectedIndex],
                    Height = Int16.Parse(heightPokemon.Text),
                    Weight = Int16.Parse(weightPokemon.Text),
                    HP = (int)HP.Value,
                    Attack = (int)Attack.Value,
                    Defense = (int)Defense.Value,
                    Speed = (int)Speed.Value,
                };

                if(type2Pokemon.SelectedIndex != -1)
                {
                    pokemon.Type2 = type2Pokemon.Items[type2Pokemon.SelectedIndex];
                }

                if (!String.IsNullOrEmpty(pokemon.Type1))
                {
                    pokemon.TypeImg1 = PokemonListViewModel.Instance.GetImageByType(pokemon.Type1);
                }
                if (!String.IsNullOrEmpty(pokemon.Type2))
                {
                    pokemon.TypeImg2 = PokemonListViewModel.Instance.GetImageByType(pokemon.Type2);
                }
                pokemon.IsFromApi = false;

                PokemonListViewModel.Instance.PokemonsList.Insert(0, pokemon);

                await App.Database.connection.InsertAsync(pokemon);
                await DisplayAlert("Succès", $"Pokémon : {pokemon.Name} ajouté !", "Ok");


                nomPokemon.Text = null;
                selectionImage.Source = "ajout.png";
                selectedImagePath = null;
                heightPokemon.Text = null;
                weightPokemon.Text = null;
                type1Pokemon.SelectedIndex = -1;
                type2Pokemon.SelectedIndex = -1;
                HP.Value = 1;
                Attack.Value = 1;
                Defense.Value = 1;
                Speed.Value = 1;
            }
            else
            {
                await DisplayAlert("Erreur", $"Impossible d'ajouter un pokémon avec des caractéristiques vides !", "Ok");
            }
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