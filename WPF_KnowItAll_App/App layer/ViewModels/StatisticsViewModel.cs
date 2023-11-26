using System;
using System.Collections.ObjectModel;
using System.Linq;
using WPF_KnowItAll_App.App_layer.Constants;
using WPF_KnowItAll_App.App_layer.Navigator;
using WPF_KnowItAll_App.App_layer.Services;
using WPF_KnowItAll_App.Data_layer.Models;
using WPF_KnowItAll_App.UI.Pages;

namespace WPF_KnowItAll_App.App_layer.ViewModels
{
    public class StatisticsViewModel : ObserverItem
    {
        private MainViewModel mainViewModel;

        private Quiz quiz;
        public Quiz Quiz
        {
            get => quiz;
            set
            {
                quiz = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<QuizResult> quizResults;
        public ObservableCollection<QuizResult> QuizResults
        {
            get => quizResults;
            set
            {
                quizResults = value;
                OnPropertyChanged();
            }
        }

        public StatisticsViewModel(MainViewModel mainViewModel, Quiz? quiz)
        {
            this.mainViewModel = mainViewModel;
            Quiz = quiz;

            try
            {
                QuizResults = JsonService.Load<ObservableCollection<QuizResult>>(Filenames.QuizResults);
                if(quiz != null)
                {
                    QuizResults = new ObservableCollection<QuizResult>(QuizResults.Where(q => q.QuizName == Quiz.Name)
                        .OrderByDescending(q => (double)q.Score / (double)q.TotalQuestions));
                }
                else
                {
                    QuizResults = new ObservableCollection<QuizResult>(QuizResults.Where(q => q.Name == mainViewModel.User.Name &&
                                                                                         q.Lastname == mainViewModel.User.LastName));
                }
            }
            catch (Exception ex)
            {
                QuizResults = new ObservableCollection<QuizResult>();
                JsonService.Save(Filenames.QuizResults, QuizResults);
            }
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ??
                    (backCommand = new RelayCommand(obj =>
                    {
                        NavigatorObject.Switch(new MainPage(), mainViewModel);
                    }));
            }
        }
    }
}
