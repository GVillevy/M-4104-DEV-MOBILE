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
            "fire",
            "water",
            "bug",
            "grass",
            "dragon"

        };
            return types;
        }
    }
    
}
