using SQLite;
using System;
using System.Collections.Generic;

namespace pokeworld.Models
{
    [Table("Pokemon")]
    public class PokemonModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string BackgroundColor { get; set; }


    }

}
