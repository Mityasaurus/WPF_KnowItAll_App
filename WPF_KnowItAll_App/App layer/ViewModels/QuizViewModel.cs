using System;
using System.Collections.ObjectModel;
using WPF_KnowItAll_App.App_layer.Constants;
using WPF_KnowItAll_App.App_layer.Navigator;
using WPF_KnowItAll_App.App_layer.Services;
using WPF_KnowItAll_App.Data_layer.Models;
using WPF_KnowItAll_App.UI.Pages;

namespace WPF_KnowItAll_App.App_layer.ViewModels
{
    public class QuizViewModel : ObserverItem
    {
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

        private int currentQuestionNumber;
        public int CurrentQuestionNumber
        {
            get => currentQuestionNumber;
            set
            {
                currentQuestionNumber = value;
                OnPropertyChanged();
            }
        }

        private Question question;
        public Question Question
        {
            get => question;
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        private QuizResult quizResult;
        private MainViewModel parentViewModel;

        public QuizViewModel(Quiz quiz, MainViewModel mainViewModel)
        { 
            Quiz = quiz;
            parentViewModel = mainViewModel;

            CurrentQuestionNumber = 1;
            Question = quiz.Questions[0];

            quizResult = new QuizResult();
            quizResult.QuizName = Quiz.Name;
            quizResult.QuizTopic = Quiz.Topic;
            quizResult.Name = mainViewModel.User.Name;
            quizResult.Lastname = mainViewModel.User.LastName;
            quizResult.TotalQuestions = Quiz.Questions.Count;
        }

        private RelayCommand answerCommand;
        public RelayCommand AnswerCommand
        {
            get
            {
                return answerCommand ??
                    (answerCommand = new RelayCommand(ans =>
                    {
                        if(int.Parse((string)ans) == question.AnswerNumber)
                        {
                            ++quizResult.Score;
                        }
                        if (CurrentQuestionNumber == quiz.Questions.Count)
                        {
                            try
                            {
                                var results = JsonService.Load<ObservableCollection<QuizResult>>(Filenames.QuizResults);
                                results.Insert(0, quizResult);
                                JsonService.Save(Filenames.QuizResults, results);
                            }
                            catch (Exception ex)
                            {
                                var results = new ObservableCollection<QuizResult>
                                {
                                    quizResult
                                };
                                JsonService.Save(Filenames.QuizResults, results);
                            }

                            NavigatorObject.Switch(new Statistics(), new StatisticsViewModel(parentViewModel, null));

                            return;
                        }
                        Question = Quiz.Questions[CurrentQuestionNumber];
                        ++CurrentQuestionNumber;
                    }));
            }
        }
    }
}
