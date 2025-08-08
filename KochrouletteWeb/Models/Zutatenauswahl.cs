using System.Text.Json;

namespace KochrouletteWeb.Models
{
    public class Zutatenauswahl
    {
        private List<Zutat> _zutaten = [];

        public Zutatenauswahl() { }

        public void Load(string path)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new ZutatJsonConverter());

            string json = File.ReadAllText(path);
            _zutaten = JsonSerializer.Deserialize<List<Zutat>>(json, options)!;
        }

        public Zutat Zufallszutat(Zutat.Kategorie kategorie, bool nurVegetarisch = false)
        {
            var filtered = _zutaten.Where(z => z.Art == kategorie).ToList();
            if (nurVegetarisch)
                filtered = filtered.Where(z => z.IsVegetarisch).ToList();

            if (!filtered.Any())
                throw new InvalidOperationException("Keine passenden Elemente gefunden.");

            Random rnd = new();
            return filtered[rnd.Next(filtered.Count)];
        }
    }
}
