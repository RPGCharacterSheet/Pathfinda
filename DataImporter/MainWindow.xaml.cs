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
        }

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
                if (row.Value[data.Columns["source"]].ToLower() == "pfrpg core")
                    code += Environment.NewLine + "new Spell(){ Name = \"" + row.Value[data.Columns["name"]] + "\", Description = @\"" + row.Value[data.Columns["full_text"]].Replace("\"", "\"\"") + "\"},";
            }
            code += Environment.NewLine + "};";
            code += @"
                }
                return _spells;
            }
        }
    }
}";
            System.IO.File.WriteAllText("Spells.cs", code);
            result.Text = "Spells.cs written to disk. Copy it into your project folder.";
        }
    }
}
