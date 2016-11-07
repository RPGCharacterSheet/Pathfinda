using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // sources: 
            // http://www.pathfindercommunity.net/home/databases
        }

        // spells
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // column headers:
            // name school subschool descriptor spell_level casting_time components costly_components range area effect targets duration dismissible shapeable saving_throw spell_resistence description description_formated source full_text verbal somatic material focus divine_focus sor wiz cleric druid ranger bard paladin alchemist summoner witch inquisitor oracle antipaladin magus adept deity SLA_Level domain short_description acid air chaotic cold curse darkness death disease earth electricity emotion evil fear fire force good language_dependent lawful light mind_affecting pain poison shadow sonic water linktext id material_costs bloodline patron mythic_text augmented mythic bloodrager shaman psychic medium mesmerist occultist spiritualist skald investigator hunter haunt_statistics ruse draconic meditative 
            string csv = "";
            using (WebClient wc = new WebClient())
            {
                csv = wc.DownloadString("https://spreadsheets.google.com/pub?key=0AhwDI9kFz9SddG5GNlY5bGNoS2VKVC11YXhMLTlDLUE&output=csv");
            }
            var data = CSVParser.Parse(csv);
            string code = "";
            code = @"using System.Collections.Generic;

namespace Pathfinda
{
    public static partial class ImportedData
    { 
        public class Spell
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
        private static List<Spell> _spells = null;
        public static List<Spell> Spells
        {
            get
            {
                if (_spells == null)
                {
                    _spells = new List<Spell>()
                    {";
            foreach (var row in data.Rows)
            {
                //if (row.Value[data.Columns["source"]].ToLower() == "pfrpg core")
                    code += Environment.NewLine + "new Spell(){ Name = \"" + row.Value[data.Columns["name"]] + "\", Description = @\"" + row.Value[data.Columns["full_text"]].Replace("\"", "\"\"") + "\"},";
            }
            code += @"
                    };
                }
                return _spells;
            }
        }
    }
}";
            System.IO.File.WriteAllText("Spells.cs", code);
            result.Text = "Spells.cs written to disk. Copy it into your project folder.";
        }

        // feats
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // columns:
            // id, name, type, description, prerequisites, prerequisite_feats, benefit, normal, special, source, fulltext, teamwork, critical, grit, style, performance, racial, companion_familiar ,race_name, note, goal, completion_benefit, multiples, suggested_traits, prerequisite_skills, panache, betrayal, targeting, esoteric, stare, weapon_mastery, item_mastery, armor_mastery, shield_mastery, blood_hex
            string csv = "";
            using (WebClient wc = new WebClient())
            {
                csv = wc.DownloadString("https://docs.google.com/spreadsheet/pub?key=0AhwDI9kFz9SddEJPRDVsYVczNVc2TlF6VDNBYTZqbkE&output=csv");
            }
            var data = CSVParser.Parse(csv);
            string code = "";
            code = @"using System.Collections.Generic;

namespace Pathfinda
{
    public static partial class ImportedData
    {
        public class Feat
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private static List<Feat> _feats = null;
        public static List<Feat> Feats
        {
            get
            {
                if (_feats == null)
                {
                    _feats = new List<Feat>()
                    {";
            foreach (var row in data.Rows)
            {
                //if (row.Value[data.Columns["source"]].ToLower() == "pfrpg core")
                    code += Environment.NewLine + "new Feat(){ Name = \"" + row.Value[data.Columns["name"]] + "\", Description = @\"" + row.Value[data.Columns["fulltext"]].Replace("\"", "\"\"") + "\"},";
            }
            code += @"
                    };
                }
                return _feats;
            }
        }
    }
}";
            System.IO.File.WriteAllText("Feats.cs", code);
            result.Text = "Feats.cs written to disk. Copy it into your project folder.";
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            result.Text = "Processing Spells.";
            await Task.Delay(500);
            Button_Click(null, null);
            result.Text = "Spells done. Next is Feats.";
            await Task.Delay(500);
            Button_Click_2(null, null);
            result.Text = "All done!";
        }
    }
}
