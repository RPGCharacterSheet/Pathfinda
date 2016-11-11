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
        private ObservableCollection<Character> _characters = null;
        public ObservableCollection<Character> Characters
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
            }
        }

        private User.UserModel _user;

        public User.UserModel User
        {
            get { return _user; }
            set { _user = value; Notify("User"); }
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
            User = MongoModels.Models.User.newUser(username, password);
            Character = CharacterClass.Create();
            Character.Owner = User._id;
            Characters = new ObservableCollection<Character>() { Character };
        }

        private void LoginWindow_LoginAttempt(string username, string password)
        {
            User = MongoModels.Models.User.getUser(username, password);
            if (User != null)
            {
                Characters = new ObservableCollection<Character>(CharacterClass.getUserCharacters(User));
                if (Characters == null)
                {
                    Character = CharacterClass.Create();
                    Character.Owner = User._id;
                    Characters = new ObservableCollection<Character> { Character };
                }
                else
                {
                    Character = Characters[0];
                }
            }
            else
            {
                Console.WriteLine("bad user or password");
            }
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://paizo.com/pathfinderRPG/prd/ultimateCampaign/campaignSystems/alignment.html");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await CharacterClass.Put(Character);
        }

        private void LoadCharacterButton(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            Character = Characters.Where(x => x.Name == button.Content.ToString()).FirstOrDefault();
        }

        private void AddCharacterButton(object sender, RoutedEventArgs e)
        {
            Character = CharacterClass.Create();
            Character.Owner = User._id;
            Characters.Add(Character);
        }
    }
}
