using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HistLib;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for ResultsScreen.xaml
    /// </summary>
    public partial class ResultsScreen : Window
    {
        public ResultsScreen()
        {
            InitializeComponent();
        }
        public ResultsScreen(Test CurrentTest, Statistics Stats, string[] UserAnswers, int Time)
        {
            InitializeComponent();
            for (int i = 0; i < CurrentTest.AllQuestionsAnswers.Count; i++)
            {
                int Numb = i + 1;
                string CurAnswer = UserAnswers[i];
                string CurCorAnswer = CurrentTest.AllQuestionsAnswers[i].answer.GetAnswer();
                this.ResList.Items.Add(new MyItem { Id = Numb, Ans = CurAnswer, CorAns = CurCorAnswer });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public class MyItem //да да костыль, может потом переделаю. листвью омерзителен
        {
            public int Id { get; set; }

            public string Ans { get; set; }
            public string CorAns { get; set; }
        }
    }
    
}
