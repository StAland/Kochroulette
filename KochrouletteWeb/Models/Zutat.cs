namespace KochrouletteWeb.Models
{
    public class Zutat
    {
        public enum Kategorie
        {
            Protein,
            Saettigungsbeilage,
            Gemuese,
            Obst
        }

        public string Name { get; set; }
        public Kategorie Art { get; set; }
        public bool IsVegetarisch { get; set; } = true;

        public Zutat() { }

        public Zutat(string name, Kategorie art, bool isVegetarisch = true)
        {
            Name = name;
            Art = art;
            IsVegetarisch = isVegetarisch;
        }
    }
}
