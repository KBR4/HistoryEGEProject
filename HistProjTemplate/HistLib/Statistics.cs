namespace HistLib
{
    public class Statistics //класс статистики - хранит информацию о количестве вопросов в данном тесте, число правильных и номер текущего вопроса
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Counter { get; set; }
        public int[] CorrectAnswerNumbers { get; set; }
        public int TotalPoints { get; set; }
        public Statistics(int t)
        {
            TotalQuestions = t;
            CorrectAnswerNumbers = new int[t];
            CorrectAnswers = 0;
            Counter = 0;
            TotalPoints = 0;
        }
    }
}
