using MongoModels.Models;
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
            Loaded += MainWindow_Loaded;
        }
        private userModel user = null;
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
            user = MongoModels.User.UserModel.newUser(username, password);
            Character = Character.Get(user, "user token plus character id maybe?");
        }

        private void LoginWindow_LoginAttempt(string username, string password)
        {
            user = MongoModels.User.UserModel.getUser(username, password);
            Character = Character.Get(user, "user token plus character id maybe?");
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
