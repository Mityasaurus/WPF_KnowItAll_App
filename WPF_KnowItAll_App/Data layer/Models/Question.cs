using System.Collections.ObjectModel;

namespace WPF_KnowItAll_App.Data_layer.Models
{
    public class Question : ObserverItem
    {
        private string text;
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> options;
        public ObservableCollection<string> Options
        {
            get => options;
            set
            {
                options = value;
                OnPropertyChanged();
            }
        }

        private int answerNumber;
        public int AnswerNumber
        {
            get => answerNumber;
            set
            {
                answerNumber = value;
                OnPropertyChanged();
            }
        }
    }
}
