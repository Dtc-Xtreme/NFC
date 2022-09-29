namespace WebAPI.Models
{
    public class CalendarItem
    {
        private DateTime date;
        private string title;
        private string description;

        public CalendarItem()
        {

        }
        public CalendarItem(DateTime date, string title, string description)
        {
            this.date = date;
            this.title = title;
            this.description = description;
        }

        public DateTime Date { get => date; set => date = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
    }
}
