using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using WPF_KnowItAll_App.App_layer.Constants;
using WPF_KnowItAll_App.App_layer.Navigator;
using WPF_KnowItAll_App.App_layer.Services;
using WPF_KnowItAll_App.Data_layer.Models;
using WPF_KnowItAll_App.UI.Pages;

namespace WPF_KnowItAll_App.App_layer.ViewModels
{
    public class MainViewModel : ObserverItem
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

        private ObservableCollection<Quiz> quizzes;
        public ObservableCollection<Quiz> Quizzes
        {
            get => quizzes;
            set
            {
                quizzes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Quiz> currentQuizzes;
        public ObservableCollection<Quiz> CurrentQuizzes
        {
            get => currentQuizzes;
            set
            {
                currentQuizzes = value;
                OnPropertyChanged();
            }
        }

        private Quiz selectedQuiz;
        public Quiz SelectedQuiz
        {
            get => selectedQuiz;
            set
            {
                selectedQuiz = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> categories;
        public ObservableCollection<string> Categories
        {
            get => categories;
            set
            {
                categories = value;
                categories.Insert(0, "Show all");
                OnPropertyChanged();
            }
        }

        private string selectedCategory;
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel(User user)
        {
            User = user;
            try
            {
                string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                Uri uri = new Uri(Path.Combine(solutionDirectory, $"DataLayer/{Filenames.QuizzesFilename}"));
                string path = uri.AbsolutePath;


                Quizzes = JsonService.Load<ObservableCollection<Quiz>>(path);
                Categories = new ObservableCollection<string>(Quizzes.Select(q => q.Topic).Distinct());
                CurrentQuizzes = Quizzes;
            }
            catch (Exception ex)
            {
                Quizzes = new ObservableCollection<Quiz>();
                JsonService.Save(Filenames.QuizzesFilename, Quizzes);
            }
        }

        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get
            {
                return logOutCommand ??
                    (logOutCommand = new RelayCommand(obj =>
                    {
                        NavigatorObject.Switch(new LoginWindow(), new LoginViewModel());
                    }));
            }
        }

        private RelayCommand selectCategoryCommand;
        public RelayCommand SelectCategoryCommand
        {
            get
            {
                return selectCategoryCommand ??
                    (selectCategoryCommand = new RelayCommand(obj =>
                    {
                        SelectedCategory = obj as string;
                        if (SelectedCategory == "Show all")
                        {
                            CurrentQuizzes = Quizzes;
                        }
                        else
                        {
                            CurrentQuizzes = new ObservableCollection<Quiz>(Quizzes.Where(q => q.Topic == SelectedCategory));
                        }
                    }));
            }
        }

        private RelayCommand startQuizCommand;
        public RelayCommand StartQuizCommand
        {
            get
            {
                return startQuizCommand ??
                    (startQuizCommand = new RelayCommand(obj =>
                    {
                        if(SelectedQuiz != null)
                        {
                            NavigatorObject.Switch(new QuizWindow(), new QuizViewModel(SelectedQuiz, this));
                        }
                    }));
            }
        }

        private RelayCommand personalStatCommand;
        public RelayCommand PersonalStatCommand
        {
            get
            {
                return personalStatCommand ??
                    (personalStatCommand = new RelayCommand(obj =>
                    {
                        NavigatorObject.Switch(new Statistics(), new StatisticsViewModel(this, null));
                    }));
            }
        }

        private RelayCommand quizStatCommand;
        public RelayCommand QuizStatCommand
        {
            get
            {
                return quizStatCommand ??
                    (quizStatCommand = new RelayCommand(obj =>
                    {
                        if(SelectedQuiz != null)
                        {
                            NavigatorObject.Switch(new Statistics(), new StatisticsViewModel(this, SelectedQuiz));
                        }
                    }));
            }
        }
    }
}
