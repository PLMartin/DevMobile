using SQLite;

namespace MyPantry.Models
{
    public class Food
    {
        [PrimaryKey]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

    }
}