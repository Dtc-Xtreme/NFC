using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.Models
{
    public class Score : BaseModel
    {
        private int score1;
        private int score2;

        public Score(int score1, int score2)
        {
            this.score1 = score1;
            this.score2 = score2;
        }

        public int Score1
        {
            get { return this.score1; }
            set
            {
                this.score1 = value;
                OnPropertyChanged();
            }
        }
        public int Score2
        {
            get { return this.score2; }
            set
            {
                this.score2 = value;
                OnPropertyChanged();
            }
        }

        public override string? ToString()
        {
            return Score1 + " / " + Score2;
        }
    }
}
