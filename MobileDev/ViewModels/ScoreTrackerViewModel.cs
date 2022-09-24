using MobileDev.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileDev.ViewModels
{
    public partial class ScoreTrackerViewModel : BaseViewModel
    {
        private int home;
        private int visitor;
        private ObservableCollection<Score> results;
        private bool saveIsEnabled;
        private string result;

        public ICommand PlusCommand { get; set; }
        public ICommand MinCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ClearScoreCommand { get; set; }
        public ICommand ClearResultsCommand { get; set; }

        public ScoreTrackerViewModel(IServiceProvider prov)
        {
            PlusCommand = new Command<string>(Plus);
            MinCommand = new Command<string>(Min);
            SaveCommand = new Command(Save);
            ClearScoreCommand = new Command(ClearScore);
            ClearResultsCommand = new Command(ClearResults);
            Home = 0;
            Visitor = 0;
            Results = new ObservableCollection<Score>();
        }

        public int Home
        {
            get { return this.home; }
            set
            {
                this.home = value;
                OnPropertyChanged();
            }
        }
        public int Visitor
        {
            get { return this.visitor; }
            set
            {
                this.visitor = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Score> Results
        {
            get { return this.results; }
            set
            {
                this.results = value;
                OnPropertyChanged();
            }
        }
        public bool SaveIsEnabled
        {
            get { return this.saveIsEnabled; }
            set
            {
                this.saveIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public string Result
        {
            get { return this.result; }
            set
            {
                this.result = value;
                OnPropertyChanged();
            }
        }

        private void Plus(string arg)
        {
            if (arg == "Home")
            {
                if (Home < 13) Home++;
            }
            else if (arg == "Visitor")
            {
                if (Visitor < 13) Visitor++;
            }

            if (Home == 13 || Visitor == 13) SaveIsEnabled = true;
        }
        private void Min(string arg)
        {
            if (arg == "Home")
            {
                if (Home > 0) Home--;
            }
            else if (arg == "Visitor")
            {
                if (Visitor > 0) Visitor--;
            }
            if (Home != 13 || Visitor != 13) SaveIsEnabled = false;
        }
        private void Save()
        {
            if (Home == 13 || Visitor == 13)
            {
                Results.Add(new Score(Home, Visitor));
                int m = 0;
                int p = 0;
                foreach (Score score in Results)
                {
                    if (score.Score1 == 13)
                    {
                        m += 1;
                    }
                    p += score.Score1 - score.Score2;
                }
                Result = m + " match(es) " + p + " points";
                ClearScore();
            }
        }
        private void ClearScore()
        {
            Home = 0;
            Visitor = 0;
            SaveIsEnabled = false;
        }
        private void ClearResults()
        {
            Results.Clear();
            Result = string.Empty;
        }
    }
}
