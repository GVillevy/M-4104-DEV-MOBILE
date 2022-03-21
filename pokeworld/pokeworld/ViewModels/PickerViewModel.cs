using System;
using System.Collections.Generic;
using System.Text;

namespace pokeworld.ViewModels
{
    internal class PickerViewModel
    {
        public List<String> TypeList { get; set; }

        public PickerViewModel()
        {
            TypeList = GetTypes();
        }

        public List<String> GetTypes()
        {
            var types = new List<String>()
        {
            "bug",
            "dark",
            "dragon",
            "electric",
            "fairy",
            "fighting",
            "fire",
            "flying",
            "ghost",
            "grass",
            "ground",
            "ice",
            "normal",
            "poison",
            "psychic",
            "rock",
            "steel",
            "water",

        };
            return types;
        }
    }
    
}
