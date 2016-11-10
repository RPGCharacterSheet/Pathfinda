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

namespace Pathfinda
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public event Action<string, string> LoginAttempt = null;
        public event Action<string, string> NewUserAttempt = null;

        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            username.Focus();
        }

        private void Finish()
        {
            LoginAttempt?.Invoke(username.Text, password.Password);
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
            NewUserAttempt?.Invoke(username.Text, password.Password);
        }
    }
}
