using System.Windows.Controls;

namespace WPF_KnowItAll_App.App_layer.Navigator
{
    public static class NavigatorObject
    {
        public static MainWindow? pageSwitcher;

        public static void Switch(UserControl newPage, object dataContext)
        {
            pageSwitcher?.Navigate(newPage, dataContext);
        }
    }
}
