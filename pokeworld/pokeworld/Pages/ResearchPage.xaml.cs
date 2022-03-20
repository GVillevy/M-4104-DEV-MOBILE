using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pokeworld.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeworld.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResearchPage : ContentPage
    {
        public ResearchPage()
        {        
            InitializeComponent();
            BindingContext = ResearchPokemonViewModel.Instance;
            SearchPokemon.ItemsSource = ResearchPokemonViewModel.Instance.PokemonReasarched;
        }
     
    }
}