using SQLite;

namespace pokeworld.Models
{
    [Table("Pokemon")]
    /*
     * Class qui représente notre model de nos pokemon avec ses caractéristiques
     */
    public class PokemonModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string TypeImg1 { get; set; }
        public string TypeImg2 { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public string BackgroundColorByType { get; set; }
        public bool IsFromApi { get; set; }
    }
}