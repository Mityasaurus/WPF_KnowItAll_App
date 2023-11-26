using System.Windows;
using System.Windows.Controls;
using WPF_KnowItAll_App.App_layer.ViewModels;

namespace WPF_KnowItAll_App.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : UserControl
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel loginVM)
            {
                loginVM.User.Password = tb_Password.Password;
            }
        }
    }
}
