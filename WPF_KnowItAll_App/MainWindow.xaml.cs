using System.Windows;
using System.Windows.Controls;
using WPF_KnowItAll_App.App_layer.Navigator;
using WPF_KnowItAll_App.App_layer.ViewModels;
using WPF_KnowItAll_App.UI.Pages;

namespace WPF_KnowItAll_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CenterWindowOnScreen();

            NavigatorObject.pageSwitcher = this;

            NavigatorObject.Switch(new RegistrationWindow(), new RegistrationViewModel());
        }

        public void Navigate(UserControl nextPage, object dataContext)
        {
            nextPage.DataContext = dataContext;
            this.Content = nextPage;
            this.DataContext = dataContext;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double windowWidth = Width;
            double windowHeight = Height;

            Left = (screenWidth - windowWidth) / 2;
            Top = (screenHeight - windowHeight) / 2;
        }
    }
}
