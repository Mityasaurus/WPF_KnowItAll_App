namespace WPF_KnowItAll_App.Data_layer.Models
{
    public class QuizResult : ObserverItem
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string lastname;
        public string Lastname
        {
            get => lastname;
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }

        public string Fullname
        {
            get => $"{Name} {Lastname}";
        }

        private string quizName;
        public string QuizName
        {
            get => quizName;
            set
            {
                quizName = value;
                OnPropertyChanged();
            }
        }

        private string quizTopic;
        public string QuizTopic
        {
            get => quizTopic;
            set
            {
                quizTopic = value;
                OnPropertyChanged();
            }
        }

        private int score;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }

        private int totalQuestions;
        public int TotalQuestions
        {
            get => totalQuestions;
            set
            {
                totalQuestions = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => $"{Score}/{TotalQuestions}";
        }
    }

}
