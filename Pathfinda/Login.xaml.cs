using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MongoModels.Models;
namespace Pathfinda
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public delegate User.UserModel LoginAttempt(string username, string password);
        private LoginAttempt login;
        public delegate User.UserModel NewUserAttempt(string username, string password);
        private NewUserAttempt newUser;
        public User user = new User();
        public Login()
        {
            login = (username, password) => user.getUser(username, password);
            newUser = (string username, string password) => user.newUser(username, password);
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            username.Focus();
        }

        private void Finish()
        {
            login?.Invoke(username.Text, password.Password);
        }

        private void text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Finish();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finish();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var NewUser = newUser?.Invoke(username.Text, password.Password);
            if(NewUser != null)
            {
                Console.WriteLine("Logged In!");
                //Console.WriteLine(NewUser._id);
                Console.WriteLine(NewUser.UserName);
                Console.WriteLine(NewUser.Password);

            }
        }
    }
}
