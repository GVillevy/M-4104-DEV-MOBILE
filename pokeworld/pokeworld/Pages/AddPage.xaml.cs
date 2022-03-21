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
        public string chemin = "";

        public AddPage()
        {
            InitializeComponent();
            BindingContext = new PickerViewModel();
        }

        /* 
         * Fonction appelée au clique de du bouton "ajouter"
         * permettant de créer un nouveau pokémon
         */
        private async void OnNewButtonClicked(object sender, EventArgs e)
        {
            /* 
             * On vérifie si nos différents éléments récupérant nos valeurs (entry/slider) sont non null
             */
            if (!string.IsNullOrWhiteSpace(nomPokemon.Text) && !string.IsNullOrWhiteSpace(chemin) && type1Pokemon.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(heightPokemon.Text) && !string.IsNullOrWhiteSpace(weightPokemon.Text))
            {
                /* 
                 * Création d'un nouveau pokémon ayant comme caractéristiques les valeurs entrées par les éléments xaml
                 */
                PokemonModel pokemon = new PokemonModel
                {
                    Name = nomPokemon.Text,
                    Image = chemin,
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

                /* 
                 * On ajoute notre pokémon dans le pokémonList du PokemonListViewModel ainsi qu'en base de donnée
                 */

                PokemonListViewModel.Instance.PokemonsList.Insert(0, pokemon);
                await App.Database.connection.InsertAsync(pokemon);

                await DisplayAlert("Succès", $"Pokémon : {pokemon.Name} ajouté !", "Ok");

                /* 
                 * Remise à zéro ou null des différents éléments entry/slider dans notre addPage
                 */

                nomPokemon.Text = null;
                selectionImage.Source = "ajout.png";
                chemin = null;
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

        /*
         * Fonction appelée au clique de l'image sur le addPage
         */
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //On initalise notre CrossMedia (nugget)
            await CrossMedia.Current.Initialize();

            //On traite notre première erreur vérifiant que la photo peut être supportée
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Erreur", "Cette fonctionnalité n'est pas supportée", "Ok");
                return;
            }
            //On créé un mediaOptions de type PickMediaOptions comportant une taille de photo moyenne
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            //On récupère le chelin d'accès de l'image
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            //On vérifié que notre chemin d'accès n'est pas null
            if (selectedImageFile == null)
            {
                await DisplayAlert("Erreur", "Aucune image sélectionnée", "Ok");
                return;
            }

            selectionImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
            chemin = selectedImageFile.Path;
        }
    }
}