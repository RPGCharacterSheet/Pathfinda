using Pathfinda.SaveData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace Pathfinda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<string> Alignments
        {
            get 
            {
                var returnValue = new ObservableCollection<string>(Enum.GetValues(typeof(Alignments)).OfType<Alignments>().Select(x => x.ToSentence()));
                return returnValue;
            }
        }

        private Character _character = null;
        public Character Character
        {
            get
            {
                return _character;
            }
            set
            {
                _character = value;
                Notify("Character");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Character = new Character();
        }


        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://paizo.com/pathfinderRPG/prd/ultimateCampaign/campaignSystems/alignment.html");
        }
    }
}
