﻿using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using HistLib;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///    
    public partial class MainWindow : Window
    {
        private Test CurrentTest;               //активный тест
        private bool IsTestActive;              //есть ли активный тест сейчас
        private bool IsTestChosen;              //выбран ли тест
        private int CurQNumber;                 //текущий номер вопроса в тесте
        private string[] UserAnswers;           //ответы юзера. Выбран массив из-за удобства обращения по индексу
        private DispatcherTimer timer;          //таймер
        int TestTime;                           //время выполнения текущего теста
        public MainWindow()
        {
            InitializeComponent();
            //Внутри региона программное добавление компонентов в окно - не используется
            #region


            //   RowDefinition Row = new RowDefinition();
            //   ColumnDefinition MapColumn = new ColumnDefinition();
            //   //ColumnDefinition ButtonColumn = new ColumnDefinition();
            //   ColumnDefinition QuestionColumn = new ColumnDefinition();
            //   MapColumn.Width = new GridLength(0.9, GridUnitType.Star);
            //   //ButtonColumn.Width = new GridLength(0.15, GridUnitType.Star);
            //   QuestionColumn.Width = new GridLength(1.1, GridUnitType.Star);

            //   MainGrid.RowDefinitions.Add(Row);
            //   MainGrid.ColumnDefinitions.Add(MapColumn);
            //   //MainGrid.ColumnDefinitions.Add(ButtonColumn);
            //   MainGrid.ColumnDefinitions.Add(QuestionColumn);
            //   MainGrid.VerticalAlignment = VerticalAlignment.Center;
            //   MainGrid.HorizontalAlignment = HorizontalAlignment.Center;
            //   //MainGrid.ShowGridLines = true;

            //   //StartTestButton = new Button();
            //   //StartTestButton.Height = 75;
            //   //StartTestButton.Width = 100;
            //   //StartTestButton.Visibility = Visibility.Hidden;
            //   //StartTestButton.HorizontalAlignment = HorizontalAlignment.Right;
            //   //StartTestButton.VerticalAlignment = VerticalAlignment.Top;

            //   //StartTestButton.Background = new SolidColorBrush(Colors.LightBlue);
            //   //StartTestButton.FontSize = 15;
            //   //StartTestButton.FontFamily = new FontFamily("Arial");
            //   //StartTestButton.VerticalContentAlignment = VerticalAlignment.Center;

            //   //StartTestButton.Content = "Начать тест";
            //   //StartTestButton.Click += StartClick;
            //   //Grid.SetRow(StartTestButton, 0);
            //   //Grid.SetColumn(StartTestButton, 1);
            //   //MainGrid.Children.Add(StartTestButton);

            //   MapImage = new Image();
            //   Grid.SetRow(MapImage, 0);
            //   Grid.SetColumn(MapImage, 0);
            //   MainGrid.Children.Add(MapImage);

            //   MapImage.Margin = new Thickness(20, 20, 20, 20);

            //   QuestionAnswerPanel = new StackPanel();
            //   QuestionAnswerPanel.VerticalAlignment = VerticalAlignment.Top;
            //   QuestionAnswerPanel.HorizontalAlignment = HorizontalAlignment.Center;
            //   QuestionAnswerPanel.Orientation = Orientation.Vertical;

            //   QuestionBlock = new TextBlock();
            //   QuestionBlock.Margin = new Thickness(0, 15, 0, 15);
            //   QuestionBlock.Padding = new Thickness(5, 5, 5, 5);
            //   QuestionBlock.Background = new SolidColorBrush(Colors.AliceBlue);
            //   QuestionBlock.FontSize = 20;
            //   QuestionBlock.TextAlignment = TextAlignment.Left;
            //   QuestionBlock.TextWrapping = TextWrapping.Wrap;
            //   QuestionBlock.FontFamily = new FontFamily("Arial");
            //   QuestionBlock.HorizontalAlignment = HorizontalAlignment.Left;
            //   QuestionBlock.MinWidth = 300;
            //   QuestionBlock.MaxWidth = 550;
            //   QuestionBlock.VerticalAlignment = VerticalAlignment.Top;

            //   AnswerBox = new TextBox();
            //   AnswerBox.MaxLength = 30;   //Макс длина ответа
            //   AnswerBox.FontFamily = new FontFamily("Arial");
            //   AnswerBox.FontSize = 15;
            //   AnswerBox.TextAlignment = TextAlignment.Left;
            //   //AnswerBox.Width = 350;
            //   //AnswerBox.Height = 25;
            //   AnswerBox.Margin = new Thickness(0, 0, 0, 15);
            //   AnswerBox.Width = QuestionBlock.Width;
            //   AnswerBox.MinWidth = 300;
            //   AnswerBox.MaxWidth = 550;
            //   AnswerBox.HorizontalAlignment = HorizontalAlignment.Left;
            //   AnswerBox.VerticalAlignment = VerticalAlignment.Top;

            //   NextQButton = new Button();
            //   NextQButton.Content = "Следующий вопрос";  //>>
            //   NextQButton.Click += NextQClick;
            //   PrevQButton = new Button();
            //   PrevQButton.Content = "Предыдущий вопрос"; //<<
            //   PrevQButton.Click += PrevQClick;
            //   EndTestButton = new Button();
            //   EndTestButton.Content = "Закончить тест";
            //   EndTestButton.Click += EndTestClick;

            //   //QuestionAnswerPanel.Children.Add(QuestionBlock);
            //   QuestionAnswerPanel.Children.Add(AnswerBox);

            //   ScrollViewer scv = new ScrollViewer();
            //   scv.Content = QuestionBlock;
            //   QuestionAnswerPanel.Children.Add(scv);
            //   scv.MaxHeight = 500;

            //   //Панель для кнопок
            //   ButtonPanel = new StackPanel();
            //   ButtonPanel.Orientation = Orientation.Horizontal;
            //   ButtonPanel.HorizontalAlignment = HorizontalAlignment.Center;
            //   ButtonPanel.MinWidth = 300;
            //   //ButtonPanel.Width = 550;
            //// чтобы переместить панель кнопок в центр пишем параметр Center, вниз - Bottom
            //   ButtonPanel.VerticalAlignment = VerticalAlignment.Bottom; 
            //   //QuestionAnswerPanel.Children.Add(ButtonPanel);

            //   ButtonPanel.Children.Add(PrevQButton);
            //   ButtonPanel.Children.Add(NextQButton);           
            //   ButtonPanel.Children.Add(EndTestButton);

            //   ButtonPanel.Margin = new Thickness(20, 0, 20, 20);
            //   NextQButton.Padding = new Thickness(5, 5, 5, 5);
            //   PrevQButton.Padding = new Thickness(5, 5, 5, 5);
            //   EndTestButton.Padding = new Thickness(5, 5, 5, 5);

            //   NextQButton.Margin = new Thickness(30, 0, 30, 0);
            //   PrevQButton.Margin = new Thickness(0, 0, 30, 0);
            //   EndTestButton.Margin = new Thickness(30, 0, 30, 0);
            //   //QuestionAnswerPanel.Children.Add(NextQButton);
            //   //QuestionAnswerPanel.Children.Add(PrevQButton);
            //   //QuestionAnswerPanel.Children.Add(EndTestButton);
            //   NextQButton.Background = new SolidColorBrush(Colors.AliceBlue);
            //   PrevQButton.Background = new SolidColorBrush(Colors.AliceBlue);
            //   EndTestButton.Background = new SolidColorBrush(Colors.AliceBlue);

            //   Grid.SetRow(QuestionAnswerPanel, 0);
            //   Grid.SetColumn(QuestionAnswerPanel, 1);
            //   Grid.SetRow(ButtonPanel, 0);
            //   Grid.SetColumn(ButtonPanel, 1);
            //   MainGrid.Children.Add(QuestionAnswerPanel);
            //   MainGrid.Children.Add(ButtonPanel);


            //   //Графическое оформление - свойства
            //   TextBlockInfo.Margin = new Thickness(15, 15, 15, 15);
            //   MainGrid.Margin = new Thickness(10, 0, 10, 10);
            //   TextBlockInfo.Background = new SolidColorBrush(Colors.AliceBlue);

            ////ImageBrush bgbrush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Images/LightBrownBackground.jpg")));
            ////this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/HistProjTemplate;component/Images/LightBrownBackground.jpg")));
            #endregion

            IsTestChosen = false;
            IsTestActive = false;
            MapImage.Visibility = Visibility.Hidden;
            QuestionAnswerPanel.Visibility = Visibility.Hidden;
            ButtonPanel.Visibility = Visibility.Hidden;
            TextBlockInfo.Text = "Для начала работы выберите тест в меню";
        }


        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }




        private void StartClick(object sender, RoutedEventArgs e)  //Кнопка начала теста
        {
            if (IsTestChosen && CurrentTest != null)    //Проверка был ли выбран тест.
            {
                if (!IsTestActive)              //Если тест еще не запущен - запустить его
                {
                    StartTest(CurrentTest);
                }
                else                            //Если тест уже запущен - проигнорировать
                {
                    MessageBox.Show("Тест уже запущен");
                }
            }
            else    //Тест еще не был выбран
            {              
                MessageBox.Show("Выберите тест в меню");
            }
        }
        private void StartTest(Test t)  //Начать заданный тест
        {
            //Картинка и панель вопроса - ответа становятся видимыми
            MapImage.Visibility = Visibility.Visible;
            QuestionAnswerPanel.Visibility = Visibility.Visible;
            ButtonPanel.Visibility = Visibility.Visible;

            //Картинка
            string imgsource = t.Source;
            MapImage.Source = new BitmapImage(new Uri(imgsource, UriKind.RelativeOrAbsolute));

            //Массив с ответами
            UserAnswers = new string[10000];    //никто же не будет создавать тест с более чем 10000 вопросов правда

            //Текущий вопрос (начинаем с первого, т.е. с 0)
            CurQNumber = 0;

            //Тест становится активным
            IsTestActive = true;

            //Панель информации показывает общее задание для подобного вида тестов
            TextBlockInfo.Text = "Рассмотрите схему и выполните задания";

            //Таймер
            TestTime = 0;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            //Показываем первый вопрос
            PrevQButton.IsEnabled = false;
            if (t.AllQuestionsAnswers.Count > 1)
            {
                NextQButton.IsEnabled = true;
            }
            else
            {
                NextQButton.IsEnabled = false;
            }
            ShowQuestion(CurQNumber, t);                      
        }
        private void timer_Tick(object sender, EventArgs e) //Таймер
        {
            TestTime++;
        }
        private void ShowQuestion(int n, Test t)    //Показать вопрос n теста t
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
            ConfirmWindow cw = new ConfirmWindow("Вы уверены, что хотите завершить тест?");
            if (cw.ShowDialog() == true)
            {
                timer.Stop();
                Statistics TestResult = CurrentTest.GetResults(UserAnswers); //Проверка ответов
                
                ResultsScreen rs = new ResultsScreen(CurrentTest, TestResult, UserAnswers, TestTime); //Окно отображения результатов
                if (rs.ShowDialog() == true)
                {

                }               
                //Возврат к исходному состоянию приложения
                MapImage.Visibility = Visibility.Hidden;
                QuestionAnswerPanel.Visibility = Visibility.Hidden;
                ButtonPanel.Visibility = Visibility.Hidden;
                IsTestActive = false;
                IsTestChosen = false;
                TextBlockInfo.Text = "Для начала работы выберите тест в меню";
            }           
        }
        private void PrevQClick(object sender, RoutedEventArgs e) //Вернуться к предыдущему вопросу
        {
            UserAnswers[CurQNumber] = AnswerBox.Text;
            NextQButton.IsEnabled = true;
            if (CurQNumber-1>=0)
            {
                CurQNumber--;
                if (CurQNumber == 0)
                {
                    PrevQButton.IsEnabled = false;
                }
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
            PrevQButton.IsEnabled = true;
            if (CurQNumber + 1 <= CurrentTest.AllQuestionsAnswers.Count - 1)
            {
                CurQNumber++;
                if (CurQNumber == CurrentTest.AllQuestionsAnswers.Count - 1)
                {
                    NextQButton.IsEnabled = false;
                }
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
                    if (CTW.ChosenTest != null)
                    {
                        CurrentTest = CTW.ChosenTest;
                        IsTestChosen = true;
                        TextBlockInfo.Text = "Вы выбрали тест " + CurrentTest.Name + ". Нажмите 'Начать тест' для прохождения работы.";
                    }                  
                }
                else
                {
                    if (!IsTestChosen)
                    {
                        TextBlockInfo.Text = "Для начала работы выберите тест в меню";
                    }
                }
            }
            else //Если есть активный тест
            {
                ConfirmWindow cw = new ConfirmWindow("Вы не завершили текущий тест.Вы уверены, что хотите выбрать другой?");
                if (cw.ShowDialog() == true)    //Если юзер ответил да - прерываем текущий тест, даем ему выбрать новый
                {
                    IsTestActive = false;                               //активный тест прерывается, даже если юзер потом нажмет назад
                    QuestionAnswerPanel.Visibility = Visibility.Hidden; //скрываем панели вопросов, картинку
                    ButtonPanel.Visibility = Visibility.Hidden;
                    MapImage.Visibility = Visibility.Hidden;

                    ChooseTestWindow CTW = new ChooseTestWindow();      //окно выбора нового теста
                    if (CTW.ShowDialog() == true)                       //если тест был выбран (выход по кнопке выбрать)
                    {
                        if (CTW.ChosenTest != null)                     //проверка на всякий случай
                        {
                            CurrentTest = CTW.ChosenTest;               //тест выбран - готов к началу
                            IsTestChosen = true;
                            TextBlockInfo.Text = "Вы выбрали тест " + CurrentTest.Name + ". Нажмите 'Начать тест' для прохождения работы.";
                        }
                        else                                        //сюда обычно не попадаем
                        {
                            IsTestChosen = false;
                            MessageBox.Show("Произошла ошибка. Перезапустите программу");
                        }
                    }
                    else                                        //если вышли из экрана выбора кнопкой назад - тест не выбран, переход к начальному состоянию
                    {
                        IsTestChosen = false;
                        TextBlockInfo.Text = "Для начала работы выберите тест в меню";
                    }
                }              
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)   //Остается пустым
        {

        }
        private void AddSection_Click(object sender, RoutedEventArgs e) //Добавление раздела
        {
            if (!IsTestActive)
            {
                PassWindow pw = new PassWindow();
                if (pw.ShowDialog() == true)
                {
                    //string EnteredPass = pw.Password;
                    //string Pass = ConfigurationManager.AppSettings["AdminPass"];
                    //if (EnteredPass == Pass)

                    string EnteredPass = pw.Password;
                    string hash = ConfigurationManager.AppSettings["AdminPass"];
                    if (VerifyHashedPassword(hash, EnteredPass))
                    {
                        MessageBox.Show("Пароль введен успешно");
                        AddWindow aw = new AddWindow();
                        if (aw.ShowDialog() == true)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала завершите текущий тест");
            }
        }
        private void AddMap_Click(object sender, RoutedEventArgs e) //Добавление тест
        {
            if (!IsTestActive)
            {
                PassWindow pw = new PassWindow();
                if (pw.ShowDialog() == true)
                {
                    //string EnteredPass = pw.Password;
                    //string Pass = ConfigurationManager.AppSettings["AdminPass"];
                    //if (EnteredPass == Pass)

                    string EnteredPass = pw.Password;
                    string hash = ConfigurationManager.AppSettings["AdminPass"];
                    if (VerifyHashedPassword(hash, EnteredPass))
                    {
                        MessageBox.Show("Пароль введен успешно");
                        AddTest aw = new AddTest();
                        if (aw.ShowDialog() == true)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала завершите текущий тест!");
            }
        }
        private void Settings(object sender, RoutedEventArgs e) //TO DO: Сборка случайного теста - окно с выбором количества вопросов
        {
            
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            if (IsTestActive)
            {
                ConfirmWindow cw = new ConfirmWindow("Вы не завершили текущий тест.Вы уверены, что хотите выйти?");
                if (cw.ShowDialog() == true)
                {
                    this.Close();
                }
            }
            else
            {
                ConfirmWindow cw = new ConfirmWindow("Вы уверены, что хотите выйти?");
                if (cw.ShowDialog() == true)
                {
                    this.Close();
                }
            }
        }
    }   
}
