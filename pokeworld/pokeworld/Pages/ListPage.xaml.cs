using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        void OnClick(object sender, ItemTappedEventArgs e)
        {
            PokemonModel current = (e.Item as PokemonModel);
            Navigation.PushAsync(new DescriptionPage(current));
            Console.WriteLine(current.Name);
        }
    }
}