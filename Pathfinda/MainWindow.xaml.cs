using MongoModels.Models;
using MongoDB.Bson;
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
        //List of available Characters
        private List<Character> _characters = null;
        public List<Character > Characters
        {
            get
            {
                return _characters;
            }
            set
            {
                _characters = value;
                Notify("Characters");
            }
        }
        //Character selected to be altered
        private Character _character;
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
                CharacterClass.PutSync(value);
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
            Loaded += MainWindow_Loaded;
        }
        private User user = new User();
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Owner = this;
            loginWindow.LoginAttempt += LoginWindow_LoginAttempt;
            loginWindow.NewUserAttempt += LoginWindow_NewUserAttempt;
            loginWindow.ShowDialog();
        }

        private void LoginWindow_NewUserAttempt(string username, string password)
        {
            var u = user.newUser(username, password);
            Character = CharacterClass.Create();
            Character.Owner = u._id;
            Characters = new List<Character>() { Character };
        }

        private void LoginWindow_LoginAttempt(string username, string password)
        {
            var u = user.getUser(username, password);
            if(u != null)
            {
                Characters = CharacterClass.getUserCharacters(u);
                if(Characters == null)
                {
                    Character = CharacterClass.Create();
                    Characters = new List<Character> { Character };
                }
            } else
            {
                Console.WriteLine("bad user or password");
            }
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://paizo.com/pathfinderRPG/prd/ultimateCampaign/campaignSystems/alignment.html");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Character.Put();
        }
    }
}
