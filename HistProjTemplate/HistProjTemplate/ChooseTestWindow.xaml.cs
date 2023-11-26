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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for ChooseTestWindow.xaml
    /// </summary>
    public partial class ChooseTestWindow : Window
    {
        //Компоненты страницы
        private ListBox SectionList;         //Листбокс для разделов
        private ListBox TestList;            //Листбокс для тем соотвтетсвующих разделу
        private Grid MainSelectionGrid;      //Общий грид окна
        private Image PreviewIMG;            //Картинка текущего теста (карта) = превью

        //Листы для подгрузки
        private List<Sect> Sections;        //Сюда подгружаются разделы после десериализации
        private List<Test> Tests;           //Сюда можно класть тесты текущего раздела (если нужно)
        private Sect CurSection;            //Текущий выбранный раздел
        private Test CurTest;               //Текущий выбранный тест

        //Возвращаемый тест
        public Test ChosenTest;             //Этот тест передаем другому окну (т.е. на выход)


        //TO DO: Красивая обертка страницы. Marginы вокруг крайних компонентов, более красивые границы, подгонка шрифта
        public ChooseTestWindow()
        {
            InitializeComponent();
            //Программное создание элементов сетки
            #region
            MainSelectionGrid = new Grid();
            SelWindow.Content = MainSelectionGrid;
            
            RowDefinition Row = new RowDefinition();
            Row.Height = new GridLength(2.0, GridUnitType.Star);
            RowDefinition TitleRow = new RowDefinition();
            TitleRow.Height = new GridLength(0.1, GridUnitType.Star);

            ColumnDefinition SectionColumn = new ColumnDefinition();
            ColumnDefinition TestColumn = new ColumnDefinition();
            ColumnDefinition PreviewAndConfirmColumn = new ColumnDefinition();

            MainSelectionGrid.ColumnDefinitions.Add(SectionColumn);
            MainSelectionGrid.ColumnDefinitions.Add(TestColumn);
            MainSelectionGrid.ColumnDefinitions.Add(PreviewAndConfirmColumn);
            MainSelectionGrid.RowDefinitions.Add(TitleRow);
            MainSelectionGrid.RowDefinitions.Add(Row);
            
            MainSelectionGrid.ShowGridLines = true;

            

            //Верхний ряд = ряд названий
            TextBlock Title1 = new TextBlock();
            Title1.Text = "Раздел";
            Title1.FontSize = 30;
            Title1.FontFamily = new FontFamily("Arial");
            Title1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(Title1, 0);
            Grid.SetColumn(Title1, 0);
            MainSelectionGrid.Children.Add(Title1);

            TextBlock Title2 = new TextBlock();
            Title2.Text = "Тема";
            Title2.FontSize = 30;
            Title2.FontFamily = new FontFamily("Arial");
            Title2.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(Title2, 0);
            Grid.SetColumn(Title2, 1);
            MainSelectionGrid.Children.Add(Title2);

            TextBlock Title3 = new TextBlock();
            Title3.Text = "Превью";
            Title3.FontSize = 30;
            Title3.FontFamily = new FontFamily("Arial");
            Title3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(Title3, 0);
            Grid.SetColumn(Title3, 2);
            MainSelectionGrid.Children.Add(Title3);

            //Нижний ряд - ряд выбора

            //Выбор раздела
            SectionList = new ListBox();
            SectionList.FontSize = 20;
            SectionList.FontFamily = new FontFamily("Arial");

            //SectionList.Items.Add("First");
            //SectionList.Items.Add("Second");
            
            Grid.SetRow(SectionList, 1);
            Grid.SetColumn(SectionList, 0);
            MainSelectionGrid.Children.Add(SectionList);

            SectionList.SelectionChanged += ChooseSelectionSect;

            //Выбор темы из раздела
            TestList = new ListBox();
            TestList.FontSize = 18;
            TestList.FontFamily = new FontFamily("Arial");

            //TestList.Items.Add("A");
            //TestList.Items.Add("B");

            Grid.SetRow(TestList, 1);
            Grid.SetColumn(TestList, 1);
            MainSelectionGrid.Children.Add(TestList);

            TestList.SelectionChanged += ChooseSelectionTest;
            //Секция с превью и кнопкой выбора
            DockPanel PrevDock = new DockPanel();
            Grid.SetRow(PrevDock, 1);
            Grid.SetColumn(PrevDock, 2);
            MainSelectionGrid.Children.Add(PrevDock);

            TextBlock maptitle = new TextBlock();
            maptitle.HorizontalAlignment = HorizontalAlignment.Center;
            maptitle.Text = "Карта";
            maptitle.FontSize = 15;
            maptitle.FontFamily = new FontFamily("Arial");
            DockPanel.SetDock(maptitle, Dock.Top);
            PrevDock.Children.Add(maptitle);

            PreviewIMG = new Image();
            PreviewIMG.Width = 400;
            PreviewIMG.Height = 400;
            DockPanel.SetDock(PreviewIMG, Dock.Top);
            PrevDock.Children.Add(PreviewIMG);

            Button ConfirmButton = new Button();
            ConfirmButton.Content = "Выбрать";
            ConfirmButton.Width = 100;
            ConfirmButton.Height = 100;
            ConfirmButton.Click += new RoutedEventHandler(ConfirmClick);
            DockPanel.SetDock(ConfirmButton, Dock.Bottom);
            PrevDock.Children.Add(ConfirmButton);
            #endregion

            //TO DO:
            //ЗДЕСЬ ДОЛЖНА ПРОИЗВОДИТЬСЯ ПОДГРУЗКА РАЗДЕЛОВ И ТЕСТОВ 
            //МОЖНО УБРАТЬ В ОТДЕЛЬНУЮ ФУНКЦИЮ ЧТОБЫ НЕ ЗАПЫЛЯТЬ


            //Пример подгрузки. Для реализации смотреть сюда
            #region 
            // оставлю пример подгрузки (тут же сериализация разделов из примера)
            // (тут слегка мусорно и все закомменчено, но пока что не надо трогать)
            #region
            //Sections = new List<Sect>();
            //Tests = new List<Test>();

            //string smap = "/HistProjTemplate;component/Images/ExampleImage.png";
            //List<string> answers1 = new List<string>
            //{
            //    "Германия", "Италия"
            //};
            //Answer ans1 = new Answer(answers1);

            //List<string> answers2 = new List<string>
            //{
            //    "СССР", "Аргентина"
            //};
            //Answer ans2 = new Answer(answers2);

            //string squest1 = "1Укажите название страны, с которой СССР вёл войну, события которой показаны на схеме.";
            //Question q1 = new Question(squest1);

            //string squest2 = "2Укажите название страны, с которой Германия вела войну, события которой показаны на схеме.";
            //Question q2 = new Question(squest2);

            //QuestionAnswer qa1 = new QuestionAnswer(q1, ans1);
            //QuestionAnswer qa2 = new QuestionAnswer(q2, ans2);
            //List<QuestionAnswer> lqa = new List<QuestionAnswer>
            //{
            //    qa1, qa2
            //};

            //Test s1 = new Test("first", smap, lqa);
            //Test s2 = new Test("second", smap, lqa);
            //Test s3 = new Test("third", smap, lqa);
            //Test s4 = new Test("fourth", smap, lqa);
            //Test s5 = new Test("fifth", smap, lqa);
            //Tests.Add(s1);
            //Tests.Add(s2);
            //Tests.Add(s3);
            //Tests.Add(s4);
            //Tests.Add(s5);

            //List<Test> Tests2 = new List<Test>();
            //Test qq1 = new Test("FIRST", smap, lqa);
            //Test qq2 = new Test("SECOND", smap, lqa);
            //Test qq3 = new Test("THIRD", smap, lqa);
            //Test qq4 = new Test("FOURTH", smap, lqa);
            //Test qq5 = new Test("FIFTH", smap, lqa);
            //Tests2.Add(qq1);
            //Tests2.Add(qq2);
            //Tests2.Add(qq3);
            //Tests2.Add(qq4);
            //Tests2.Add(qq5);

            //Sect sect1 = new Sect("Вторая мировая война", Tests);
            //Sect sect2 = new Sect("WORLD WAR II", Tests2);
            //Sections.Add(sect1);
            //Sections.Add(sect2);

            // сериализуем разделы из примера
            //List<Sect> sects = new List<Sect>();
            //sects.Add(sect1);
            //sects.Add(sect2);

            //// получаем поток, куда будем записывать сериализованный объект
            //using (FileStream fs = new FileStream("sections.dat", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, sects);

            //    MessageBox.Show("Объект сериализован");
            //}
            #endregion
            // подгрузка элементов (десериализация из файла people.dat)
            // в дальнейшем во время запуска используем только десериализацию
            BinaryFormatter formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            Sections = new List<Sect>();
            using (FileStream fs = new FileStream("sections.dat", FileMode.OpenOrCreate))
            {
                Sections = (List<Sect>)formatter.Deserialize(fs);
                MessageBox.Show($"Объект десериализован: {Sections[0].Name}");
            }


            #endregion
            foreach (Sect s in Sections)
            {
                SectionList.Items.Add(s.ToString());
            }
        }    
        private void ChooseSelectionSect(object sender, SelectionChangedEventArgs e)    //Выбор раздела - работает при клике на айтем левого листбокса
        {
            CurSection = null;
            CurTest = null;
            PreviewIMG.Source = null;
            TestList.SelectedItem = null;
            //Скорее всего не очень безопасная функция, так как отвечает на каждое изменение выбора. Подумать о безопасности
            string s = SectionList.SelectedItem.ToString();
            CurSection = GetSectionFromListByName(Sections, s);
            TestList.Items.Clear();
            foreach (Test t in CurSection.Tests)
            {
                TestList.Items.Add(t);
            }
            
        }
        private void ChooseSelectionTest(object sender, SelectionChangedEventArgs e) //выбор темы - работает при клике на айтем листбокса
        {
            //Аналогично, потенциально опасная функция           
            
            if (CurSection != null) //без этой проверки программа падает при смене раздела после выбранной темы
                //возможно стоит задуматься о том, чтобы запретить юзерам менять раздел после выбранного теста?
                //или внимательно проверять здесь баги которые могут пробежать через этот костыль-проверку
            {
                string s = TestList.SelectedItem.ToString();
                CurTest = GetTestFromListByName(CurSection.Tests, s);
                if (CurTest != null)  //не уверен что это может произойти, но пусть будет
                {
                    ChosenTest = CurTest;
                    string imgsource = CurTest.Source;
                    PreviewIMG.Source = new BitmapImage(new Uri(imgsource, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show("Выберите раздел заново");
                }
            }
        }
        private void ConfirmClick(object sender, RoutedEventArgs e) //подтверждение, выход
        {
            if (ChosenTest != null)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Выберите тест!");
            }
        }    
        private Test GetTestFromListByName(List<Test> ls, string s) //Возвращает тест по названию
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

        private Sect GetSectionFromListByName(List<Sect> ls, string s) //Возвращает раздел по названию
        {
            foreach (Sect sect in ls)
            {
                if (sect.Name == s)
                {
                    return sect;
                }
            }
            return null;
        }
    }
}
