namespace WPF_KnowItAll_App.Data_layer.Models
{
    public class User : ObserverItem
    {
        private string name;
        private string lastname;
        private string email;
        private string password;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => lastname;
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
    }
}
