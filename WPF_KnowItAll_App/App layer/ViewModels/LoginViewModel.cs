using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPF_KnowItAll_App.App_layer.Constants;
using WPF_KnowItAll_App.App_layer.Navigator;
using WPF_KnowItAll_App.App_layer.Services;
using WPF_KnowItAll_App.Data_layer.Models;
using WPF_KnowItAll_App.UI.Pages;

namespace WPF_KnowItAll_App.App_layer.ViewModels
{
    public class LoginViewModel : ObserverItem
    {
        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<User> Users { get; set; }

        public LoginViewModel()
        {
            User = new User();
            try
            {
                Users = JsonService.Load<ObservableCollection<User>>(Filenames.UsersFilename);
            }
            catch (Exception ex)
            {
                Users = new ObservableCollection<User>();
                JsonService.Save(Filenames.UsersFilename, Users);
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        if(!DataValidatorService.IsEmailValid(User.Email))
                        {
                            MessageBox.Show("Invalid email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (!DataValidatorService.IsEmailRegistered(User.Email, Users))
                        {
                            MessageBox.Show("Email is not registered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        User loginUser = Users.Where(u => u.Email == User.Email).First();

                        if (loginUser.Password != User.Password)
                        {
                            MessageBox.Show("Invalid password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        NavigatorObject.Switch(new MainPage(), new MainViewModel(loginUser));
                    }));
            }
        }

        private RelayCommand switchToSignUpCommand;
        public RelayCommand SwitchToSignUpCommand
        {
            get
            {
                return switchToSignUpCommand ??
                    (switchToSignUpCommand = new RelayCommand(obj =>
                    {
                        NavigatorObject.Switch(new RegistrationWindow(), new RegistrationViewModel());
                    }));
            }
        }
    }
}
