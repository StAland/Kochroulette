using KochrouletteWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace KochrouletteWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty] public bool Protein { get; set; } = true;
        [BindProperty] public bool Saettigungsbeilage { get; set; } = true;
        [BindProperty] public bool Gemuese { get; set; } = true;
        [BindProperty] public bool Obst { get; set; } = false;
        [BindProperty] public bool NurVegetarisch { get; set; } = false;
        [BindProperty] public int BudgetVon { get; set; } = 5;
        [BindProperty] public int BudgetBis { get; set; } = 30;

        public string? ErgebnisProtein { get; set; }
        public string? ErgebnisSaettigungsbeilage { get; set; }
        public string? ErgebnisGemuese { get; set; }
        public string? ErgebnisObst { get; set; }
        public string? ErgebnisBudget { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var auswahl = new Zutatenauswahl();
            auswahl.Load("wwwroot/zutaten.json");

            var rand = new Random();

            if (Protein)
                ErgebnisProtein = auswahl.Zufallszutat(Zutat.Kategorie.Protein, NurVegetarisch).Name;
            if (Saettigungsbeilage)
                ErgebnisSaettigungsbeilage = auswahl.Zufallszutat(Zutat.Kategorie.Saettigungsbeilage, NurVegetarisch).Name;
            if (Gemuese)
                ErgebnisGemuese = auswahl.Zufallszutat(Zutat.Kategorie.Gemuese, NurVegetarisch).Name;
            if (Obst)
                ErgebnisObst = auswahl.Zufallszutat(Zutat.Kategorie.Obst, NurVegetarisch).Name;

            var zahl = rand.Next(BudgetVon + 1, BudgetBis - 1);
            ErgebnisBudget = $"{(zahl - 1).ToString("C", CultureInfo.GetCultureInfo("de-DE"))} bis {(zahl + 1).ToString("C", CultureInfo.GetCultureInfo("de-DE"))}";
        }
    }
}
