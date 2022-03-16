using pokeworld.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeworld.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {
        public DescriptionPage(PokemonModel pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
        }
    }
}