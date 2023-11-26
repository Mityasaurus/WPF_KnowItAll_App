using System;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_KnowItAll_App.App_layer.Constants;
using WPF_KnowItAll_App.App_layer.Navigator;
using WPF_KnowItAll_App.App_layer.Services;
using WPF_KnowItAll_App.Data_layer.Models;
using WPF_KnowItAll_App.UI.Pages;

namespace WPF_KnowItAll_App.App_layer.ViewModels
{
    public class RegistrationViewModel : ObserverItem
    {
        private User newUser;
        public User NewUser
        {
            get => newUser;
            set
            {
                newUser = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<User> Users { get; set; }

        public RegistrationViewModel()
        {
            NewUser = new User();
            try
            {
                Users = JsonService.Load<ObservableCollection<User>>(Filenames.UsersFilename);
            }
            catch(Exception ex)
            {
                Users = new ObservableCollection<User>();
                JsonService.Save(Filenames.UsersFilename, Users);
            }
        }

        private RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand ??
                    (registerCommand = new RelayCommand(obj =>
                    {
                        if(!DataValidatorService.IsNameValid(newUser.Name))
                        {
                            MessageBox.Show("Invalid name input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if(!DataValidatorService.IsNameValid(newUser.LastName))
                        {
                            MessageBox.Show("Invalid lastname input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if(!DataValidatorService.IsEmailValid(newUser.Email))
                        {
                            MessageBox.Show("Invalid email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if(!DataValidatorService.IsPasswordValid(newUser.Password))
                        {
                            MessageBox.Show("Password is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if(DataValidatorService.IsEmailRegistered(NewUser.Email, Users))
                        {
                            MessageBox.Show("Email is already registered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        Users.Add(NewUser);
                        JsonService.Save(Filenames.UsersFilename, Users);

                        SwitchToSignInCommand.Execute(null);
                    }));
            }
        }

        private RelayCommand switchToSignInCommand;
        public RelayCommand SwitchToSignInCommand
        {
            get
            {
                return switchToSignInCommand ??
                    (switchToSignInCommand = new RelayCommand(obj =>
                    {
                        NavigatorObject.Switch(new LoginWindow(), new LoginViewModel());
                    }));
            }
        }
    }
}
