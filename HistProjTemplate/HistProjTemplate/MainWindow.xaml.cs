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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HistLib;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Statistics CurrentStatistic;    //статистика для текущего теста
        private Test CurrentTest;               //активный тест
        private bool IsTestActive;              //есть ли активный тест сейчас
        private bool IsTestChosen;

        public MainWindow()
        {
            InitializeComponent();
            IsTestChosen = false;

        }
        private void OpenSelectionWindow(object sender, RoutedEventArgs e)
        {
            ChooseTestWindow OSM = new ChooseTestWindow();
            if (OSM.ShowDialog() == true)
            {
                CurrentTest = OSM.ChosenTest;
            }
        }
        private void OpenSectionMenu(object sender, RoutedEventArgs e) //Сделать это отдельным окном?
        {           
            //if (IsTestActive == true)
            //{
            //    //TO DO: Остановить выполнение всех тестов, возможно спросить хотите ли вы прервать тест? - добавить вопрос пользователю
            //    //Если да, убрать все текущие карты, остановить тест, обновить таймер
            //}

            ////Выгрузка всех разделов в листбокс для выбора раздела (переделать?)
            //SectionList.Items.Clear();
            ////foreach (Sect s in Sections)
            ////{
            ////    SectionList.Items.Add(s.Name);
            ////}
            //foreach (Test t in Tests)
            //{
            //    SectionList.Items.Add(t.Name);
            //}

            ////Показать меню выбора раздела - сваять красивее
            //SectionList.Visibility = Visibility.Visible;
            //ListBoxChoice.Visibility = Visibility.Visible;

            //MapImage.Visibility = Visibility.Hidden;
            //AnswerBlock.Visibility = Visibility.Hidden;
            //QuestionBlock.Visibility = Visibility.Hidden;
            //ConfirmAnswerButton.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddSection_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("EFEFE");
            //Добавить раздел - название
        }
        private void AddMap_Click(object sender, RoutedEventArgs e)
        {
            //Добавить карту в раздел - открыть меню выбора разделов
        }
        private void ConfirmAnswer_Click(object sender, RoutedEventArgs e)  //кнопка подтверждения ответа при активном тесте
        {
            //if (CurrentTest == null || CurrentStatistic == null)
            //{
            //    MessageBox.Show("Произошла ошибка");
            //    //ошибка
            //}
            ////Проверка правильности ответа
            ////Если правильный, записать в статистику (+1)
            //string sAnswer = AnswerBlock.Text;
            //if (CurrentTest.AllQuestionsAnswers[CurrentStatistic.Counter].answer.CheckAnswer(sAnswer) == true)
            //{
            //    CurrentStatistic.CorrectAnswers++;
            //}
            //CurrentStatistic.Counter++;

            //if (CurrentStatistic.Counter >= CurrentTest.AllQuestionsAnswers.Count)
            //{
            //    //Промежуточный показ результатов
            //    MessageBox.Show($"Тест завершен! Вы дали {CurrentStatistic.CorrectAnswers} правильных ответов из {CurrentStatistic.Counter}");
            //    //TO DO: отдельный экран с результатами.
            //    //TO DO: меню с предложением выбрать следующий тест

            //    //временная заглушка - возврат к состоянию открытия меню
            //    SectionList.Visibility = Visibility.Visible;
            //    ListBoxChoice.Visibility = Visibility.Visible;

            //    MapImage.Visibility = Visibility.Hidden;
            //    AnswerBlock.Visibility = Visibility.Hidden;
            //    QuestionBlock.Visibility = Visibility.Hidden;
            //    ConfirmAnswerButton.Visibility = Visibility.Hidden;
            //}
            //else //если есть следующий вопрос, перейти к нему.
            //{
            //    ShowQuestionByNumberFromSection(CurrentTest, CurrentStatistic.Counter);
            //}
        }
        private void Settings(object sender, RoutedEventArgs e)
        {
            //настройки. нужны?
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListBoxChoice_Click(object sender, RoutedEventArgs e)  //кнопка выбора раздела
        {
            //if (SectionList.SelectedItem != null)   //если выбран раздел из списка
            //{
            //    MessageBox.Show("Вы выбрали: " + SectionList.SelectedItem.ToString()); //дебаг сообщение, убрать в релизе

            //    //опции видимости - убрать разделы, показать области для карты, вопроса, ответа, кнопки подтверждения
            //    SectionList.Visibility = Visibility.Hidden;
            //    ListBoxChoice.Visibility = Visibility.Hidden;

            //    MapImage.Visibility = Visibility.Visible;
            //    AnswerBlock.Visibility = Visibility.Visible;
            //    QuestionBlock.Visibility = Visibility.Visible;
            //    ConfirmAnswerButton.Visibility = Visibility.Visible;

            //    CurrentTest = GetTestFromListByName(Tests, SectionList.SelectedItem.ToString());            //выбранный тест
            //    CurrentStatistic = new Statistics(CurrentTest.AllQuestionsAnswers.Count);                   //текущая статистика
            //    MapImage.Source = new BitmapImage(new Uri(CurrentTest.Source, UriKind.RelativeOrAbsolute)); //текущая карта
            //    if (CurrentTest.AllQuestionsAnswers.Count > 0)
            //    {
            //        ShowQuestionByNumberFromSection(CurrentTest, 0);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Этот раздел пуст!");
            //    }
            //}
        }
        private Test GetTestFromListByName(List<Test> ls, string s)    //возвращает тест по его названию
        {
            foreach (Test test in ls)
            {
                if (test.Name == s)
                {
                    return test;
                }
            }
            return null;
        }

        private void ShowQuestionByNumberFromSection(Test test, int n)    //показать вопрос по индексу в листе
        {
            //AnswerBlock.Text = "";
            //if (n < test.AllQuestionsAnswers.Count)
            //{
            //    Question q = test.AllQuestionsAnswers[n].question;
            //    QuestionBlock.Text = q.QuestionP;
            //}
        }

        
    }   
}
