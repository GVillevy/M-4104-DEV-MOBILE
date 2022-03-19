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
        public void OnClick(object sender, SelectionChangedEventArgs e)
        {
            PokemonModel current = e.CurrentSelection.FirstOrDefault() as PokemonModel;
            Navigation.PushAsync(new DescriptionPage(current));
            //Console.WriteLine(current.IsFromApi);
        }
    }
}