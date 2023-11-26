using System.Collections.ObjectModel;

namespace WPF_KnowItAll_App.Data_layer.Models
{
    public class Quiz : ObserverItem
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

        private string topic;
        public string Topic
        {
            get => topic;
            set
            {
                topic = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            get => questions;
            set
            {
                questions = value;
                OnPropertyChanged();
            }
        }
    }
}
