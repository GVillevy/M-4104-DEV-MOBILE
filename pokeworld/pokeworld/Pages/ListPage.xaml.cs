using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using pokeworld.Models;
using pokeworld.Pages;
using pokeworld.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeworld.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {

        public ListPage()
        {
            InitializeComponent();
            ListOfPokemon.ItemsSource = PokemonListViewModel.Instance.PokemonsList;
            BindingContext = PokemonListViewModel.Instance;

        }
        /*
         * Fonction appelée au clique d'un pokémon
         */
        public void OnClick(object sender, SelectionChangedEventArgs e)
        {
            /*
             * On récupère notre pokémon sur lequel on a cliqué
             */
            PokemonModel current = e.CurrentSelection.FirstOrDefault() as PokemonModel;
            /*
             * On créé une nouvelle page DescriptionPage prenant en paramètre notre pokémon récupéré
             */
            Navigation.PushAsync(new DescriptionPage(current));
        }
    }
}