using System;
using System.Windows;
using HistLib;

namespace HistoryMapQuiz
{
    /// <summary>
    /// Interaction logic for ResultsScreen.xaml
    /// </summary>
    public partial class ResultsScreen : Window
    {
        public ResultsScreen()
        {
            InitializeComponent();
            ResList.SelectedItem = null;
        }
        public ResultsScreen(Test CurrentTest, Statistics Stats, string[] UserAnswers, int Time)
        {
            TimeSpan t = TimeSpan.FromSeconds(Time);
            string timestring = string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Hours,
                t.Minutes,
                t.Seconds);

            InitializeComponent();
            int iUserPoints = 0;
            int iTotalPoints = 0;
            for (int i = 0; i < CurrentTest.AllQuestionsAnswers.Count; i++)
            {
                int Numb = i + 1;
                string CurAnswer = UserAnswers[i];
                string CurCorAnswer = CurrentTest.AllQuestionsAnswers[i].answer.GetAnswer();

                int CurQPoints = CurrentTest.AllQuestionsAnswers[i].Points;
                iUserPoints = iUserPoints + Stats.CorrectAnswerNumbers[i];
                iTotalPoints = iTotalPoints + CurQPoints;

                if ((Stats.CorrectAnswerNumbers[i] == 1 && CurQPoints == 1) || Stats.CorrectAnswerNumbers[i] == 2)
                {
                    
                    this.ResList.Items.Add(new MyItem { Id = Numb, Ans = CurAnswer, CorAns = CurCorAnswer, Clr = "Верно" });
                }
                else
                {
                    if (CurQPoints == 2 && Stats.CorrectAnswerNumbers[i] == 1)
                    {
                        this.ResList.Items.Add(new MyItem { Id = Numb, Ans = CurAnswer, CorAns = CurCorAnswer, Clr = "Частично верно" });
                    }
                    else
                    {
                        this.ResList.Items.Add(new MyItem { Id = Numb, Ans = CurAnswer, CorAns = CurCorAnswer, Clr = "Неверно" });
                    }
                }

            }
            ResultBlock.Text = "Тест " + CurrentTest.ToString() + " завершен. Вы набрали " + iUserPoints + " баллов из " + iTotalPoints + " возможных.  Время решения теста: " + timestring + ".";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public class MyItem //да да костыль, может потом переделаю.
        {
            public int Id { get; set; }

            public string Ans { get; set; }
            public string CorAns { get; set; }
            public string Clr { get; set; }
        }
    }
    
}
