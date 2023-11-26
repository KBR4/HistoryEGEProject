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
        private bool IsTestChosen;              //выбран ли тест
        private int CurQNumber;                 //текущий номер вопроса в тесте
        private string[] UserAnswers;           //ответы юзера. Выбран массив из-за удобства обращения по индексу

        //WPF компоненты текущего окна
        private Button StartTestButton;
        private Image MapImage;
        private TextBox AnswerBox;
        private TextBlock QuestionBlock;
        private Button PrevQButton;
        private Button NextQButton;
        private StackPanel QuestionAnswerPanel;
        private Button EndTestButton;
        public MainWindow()
        {
            InitializeComponent();
            IsTestChosen = false;
            IsTestActive = false;
            TextBlockInfo.Text = "Для начала работы выберите тест в меню";

            RowDefinition Row = new RowDefinition();
            ColumnDefinition MapColumn = new ColumnDefinition();
            ColumnDefinition ButtonColumn = new ColumnDefinition();
            ColumnDefinition QuestionColumn = new ColumnDefinition();
            MapColumn.Width = new GridLength(1.0, GridUnitType.Star);
            ButtonColumn.Width = new GridLength(0.15, GridUnitType.Star);
            QuestionColumn.Width = new GridLength(1.0, GridUnitType.Star);
            
            MainGrid.RowDefinitions.Add(Row);
            MainGrid.ColumnDefinitions.Add(MapColumn);
            MainGrid.ColumnDefinitions.Add(ButtonColumn);
            MainGrid.ColumnDefinitions.Add(QuestionColumn);
            MainGrid.VerticalAlignment = VerticalAlignment.Center;
            MainGrid.HorizontalAlignment = HorizontalAlignment.Center;
            //MainGrid.ShowGridLines = true;

            StartTestButton = new Button();
            StartTestButton.Height = 75;
            StartTestButton.Width = 100;
            StartTestButton.Visibility = Visibility.Hidden;
            StartTestButton.Content = "Начать тест";
            StartTestButton.Click += StartClick;
            Grid.SetRow(StartTestButton, 0);
            Grid.SetColumn(StartTestButton, 1);
            MainGrid.Children.Add(StartTestButton);

            MapImage = new Image();
            Grid.SetRow(MapImage, 0);
            Grid.SetColumn(MapImage, 0);
            MainGrid.Children.Add(MapImage);
            MapImage.Visibility = Visibility.Hidden;

            QuestionAnswerPanel = new StackPanel();
            QuestionAnswerPanel.VerticalAlignment = VerticalAlignment.Center;
            QuestionAnswerPanel.HorizontalAlignment = HorizontalAlignment.Center;
            QuestionAnswerPanel.Orientation = Orientation.Vertical;
            QuestionBlock = new TextBlock();
            AnswerBox = new TextBox();
            NextQButton = new Button();
            NextQButton.Content = "Следующий вопрос";  //>>
            NextQButton.Click += NextQClick;
            PrevQButton = new Button();
            PrevQButton.Content = "Предыдущий вопрос"; //<<
            PrevQButton.Click += PrevQClick;
            EndTestButton = new Button();
            EndTestButton.Content = "Закончить тест";
            EndTestButton.Click += EndTestClick;

            QuestionAnswerPanel.Children.Add(QuestionBlock);
            QuestionAnswerPanel.Children.Add(AnswerBox);
            QuestionAnswerPanel.Children.Add(NextQButton);
            QuestionAnswerPanel.Children.Add(PrevQButton);
            QuestionAnswerPanel.Children.Add(EndTestButton);

            Grid.SetRow(QuestionAnswerPanel, 0);
            Grid.SetColumn(QuestionAnswerPanel, 2);
            MainGrid.Children.Add(QuestionAnswerPanel);
            QuestionAnswerPanel.Visibility = Visibility.Hidden;

        }      
        private void StartClick(object sender, RoutedEventArgs e)  //начать тест
        {
            StartTestButton.Visibility = Visibility.Hidden;
            if (IsTestChosen && CurrentTest != null)
            {
                StartTest(CurrentTest);
            }
            else
            {              
                MessageBox.Show("Что-то пошло не так. Выберите тест заново");
            }
        }

        private void StartTest(Test t)
        {
            MapImage.Visibility = Visibility.Visible;
            QuestionAnswerPanel.Visibility = Visibility.Visible;
            string imgsource = t.Source;
            MapImage.Source = new BitmapImage(new Uri(imgsource, UriKind.RelativeOrAbsolute));
            UserAnswers = new string[1000];    //никто же не будет создавать тест с более чем 1000 вопросов правда
            CurQNumber = 0;
            ShowQuestion(CurQNumber, t);                      
        }

        private void ShowQuestion(int n, Test t)
        {
            if (n>=0 && n <= t.AllQuestionsAnswers.Count)
            {
                QuestionBlock.Text = t.AllQuestionsAnswers[n].question.ToString();
                AnswerBox.Text = UserAnswers[n];
            }           
        }
        private void EndTestClick(object sender, RoutedEventArgs e)
        {
            UserAnswers[CurQNumber] = AnswerBox.Text;
            //TO DO: спросить у юзера, точно ли мы хотим завершить тест?
            //если да, то показать статистику - отдельный экран (?)
            //функция проверки ответов
        }

        private void PrevQClick(object sender, RoutedEventArgs e)
        {
            UserAnswers[CurQNumber] = AnswerBox.Text;
            if (CurQNumber-1>=0)
            {
                CurQNumber--;
                ShowQuestion(CurQNumber, CurrentTest);
            }
            else
            {
                MessageBox.Show("Это первый вопрос!");
            }
        }

        private void NextQClick(object sender, RoutedEventArgs e)
        {
            UserAnswers[CurQNumber] = AnswerBox.Text;
            if (CurQNumber + 1 <= CurrentTest.AllQuestionsAnswers.Count - 1)
            {
                CurQNumber++;
                ShowQuestion(CurQNumber, CurrentTest);
            }
            else
            {
                MessageBox.Show("Это последний вопрос!");
            }
        }
        private void OpenSelectionWindow(object sender, RoutedEventArgs e) //Кнопка выбора теста
        {
            if (!IsTestActive)  //Если активного теста нет (не находимся в процессе выполнения)
            {
                ChooseTestWindow CTW = new ChooseTestWindow();
                if (CTW.ShowDialog() == true)
                {
                    CurrentTest = CTW.ChosenTest;
                    IsTestChosen = true;
                    StartTestButton.Visibility = Visibility.Visible;
                    TextBlockInfo.Text = "Вы выбрали тест " + CurrentTest.Name + ". Нажмите 'Начать тест' для прохождения работы.";
                }
            }
            else
            {
                //TO DO: Здесь юзердиалог хотим ли мы прекратить тест
            }
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
        private void Settings(object sender, RoutedEventArgs e)
        {
            //настройки. нужны?
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (IsTestActive)
            {
                //TO DO: если тест активен спросить юзера хочет ли он выйти
            }
            else
            {
                this.Close();
            }
        }
    }   
}
