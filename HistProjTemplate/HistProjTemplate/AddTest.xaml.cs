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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.Reflection;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        private List<Sect> Sections;
        public AddTest()
        {
            InitializeComponent();
            Sections = (List<Sect>)Deserialization();
            foreach (Sect s in Sections)
            {
                SectBox.Items.Add(s.ToString());
            }
        }


        public Sect GetSectByName(string Name)
        {
            foreach (Sect sec in Sections)
            {
                if (sec.Name == Name)
                {
                    return sec;
                }
            }
            return null;
        }

        public void Serialization(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            using (FileStream fs = new FileStream("sections.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }

        public object Deserialization()
        {
            BinaryFormatter formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            object obj = new object();
            using (FileStream fs = new FileStream("sections.dat", FileMode.OpenOrCreate))
            {
                obj = formatter.Deserialize(fs);
                return obj;
            }
        }

        public void AddTestByUser(string NameTest, string SectName)
        {
            Sections = (List<Sect>)Deserialization();
            Sect s = GetSectByName(SectName);
            string ImageSource = GetImageSource(NameTest);
            List<QuestionAnswer> QA = GetAllQuestionsAnswers();
            Test curTest = new Test(NameTest, ImageSource, QA);
            s.AddTest(curTest);
            Serialization(Sections);
        }

        // добавление изображения и ссылки на него
        public string GetImageSource(string NameTest)
        {
            BitmapImage bitmap = new BitmapImage();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите изображение";

            //фильтр для того, чтобы выбирать только из фото
            dialog.Filter =
        "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                bitmap.UriSource = new Uri(dialog.FileName);
                string pathToImages = Assembly.GetExecutingAssembly().Location;

                //задание абсолютного пути до папки с ресурсами Images 
                pathToImages = pathToImages.Remove(pathToImages.Length - 30);
                pathToImages += NameTest;
                MessageBox.Show($"File name: {dialog.FileName} \nYour path: \n{pathToImages}");
                File.Copy(dialog.FileName, System.IO.Path.Combine(pathToImages, dialog.SafeFileName));
                return pathToImages;     // абсолютный путь до изображения 
            }
            return "ОШИБКА";  // априори обычно здесь нет ошибки
                              // и пользователь выбирает какую-то картинку, либо можно вернуть null
        }

        public List<QuestionAnswer> GetAllQuestionsAnswers()
        {
            BitmapImage bitmap = new BitmapImage();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл с вопросами и ответами для теста";

            //фильтр для того, чтобы выбирать только .txt
            dialog.Filter =
        "TXT files|*.txt;|All files|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                bitmap.UriSource = new Uri(dialog.FileName);
                string pathToText = Assembly.GetExecutingAssembly().Location;
                // чтение из файла
                using (StreamReader sr = new StreamReader(pathToText))
                {
                    int j = 0;   // счетчик
                    List<string> answerList = new List<string>();       // лист ответов
                    string stringQuestion = "";                         // пустая строка, чтобы прога не ругалась на 115 строке
                    Question newQuestion = new Question(stringQuestion);// целый вопрос
                    Answer newAnswer;                                   // целый ответ
                    QuestionAnswer newQuestionAnswer;                   // объединение ответа и вопроса в один объект 
                    List<QuestionAnswer> newQuestionAnswers = new List<QuestionAnswer>();

                    if (!sr.EndOfStream)
                    {
                        while (!sr.EndOfStream)
                        {
                            string curString = sr.ReadLine();
                            if (curString[0] != 'A') // Английская
                            {
                                if (answerList != null)
                                {
                                    newAnswer = new Answer(answerList);
                                    newQuestionAnswer = new QuestionAnswer(newQuestion, newAnswer);
                                    newQuestionAnswers.Add(newQuestionAnswer);
                                }
                                answerList = new List<string>(); // обнуление листа ответов
                                if (curString[0] != 'A')
                                {
                                    stringQuestion += curString.Substring(3);
                                }
                                else
                                {
                                    stringQuestion += "\n" + curString;
                                }

                                //newQuestion = new Question(curString.Substring(3)); // 0, 1 и 2 символы это Q:_ (_ - пробел)
                            }
                            else
                            {
                                newQuestion = new Question(stringQuestion); // 0, 1 и 2 символы это Q:_ (_ - пробел)
                                stringQuestion = "";
                                if (curString[0] == 'A') // Английская
                                {
                                    answerList.Add(curString.Substring(3));
                                }
                                //else
                                //{
                                //    answerList.Add(curString);
                                //}
                            }
                            j++;
                        }
                        // так как последняя строка это A, то нужно создать еще одну пару 
                        newAnswer = new Answer(answerList);
                        newQuestionAnswer = new QuestionAnswer(newQuestion, newAnswer);
                        newQuestionAnswers.Add(newQuestionAnswer);
                        return newQuestionAnswers;
                    }
                    else
                    {
                        // нам дали пустой файл :(
                    }
                }
            }
            return null;  // априори обычно здесь нет ошибки, но на всякий случай null
        }

        private void SectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Name = SectBox.SelectedItem.ToString();
            Sect s = GetSectByName(Name);

            TestView.Items.Clear();
            foreach (Test t in s.Tests)
            {
                TestView.Items.Add(t.Name);
            }
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)    //Добавить тест - новое окно спрашивает название
        {
            PassWindow pw = new PassWindow("Введите название добавляемого теста:");
            if (pw.ShowDialog() == true)
            {
                string NewTestName = pw.Password;
                string SectName = SectBox.SelectedItem.ToString();
                AddTestByUser(NewTestName, SectName);
            }
        }

        private void RemoveTest_Click(object sender, RoutedEventArgs e)  //Удалить выбранный тест
        {
            if (TestView.SelectedItem != null)
            {
                string TestName = TestView.SelectedItem.ToString();
                string SectName = SectBox.SelectedItem.ToString();
                RemoveTestByUser(SectName, TestName);
            }
            else
            {
                MessageBox.Show("Выберите тест, который вы хотите удалить");
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)     //Назад
        {
            this.DialogResult = true;
        }

        public void RemoveTestByUser(string SectName, string TestName)  //удаление теста из раздела с известным названием
        {
            Sections = (List<Sect>)Deserialization();
            Sect sect = null;
            for (int i = 0; i < Sections.Count; i++)
            {
                if (Sections[i].Name == SectName)
                {
                    sect = Sections[i];
                    break;
                }
            }
            for (int i = 0; i<sect.Tests.Count; i++)
            {
                if (sect.Tests[i].Name == TestName)
                {
                    sect.Tests.RemoveAt(i);
                    break;
                }
            }
            TestView.Items.Clear();
            foreach (Test t in sect.Tests)
            {
                TestView.Items.Add(t.Name);
            }
            Serialization(Sections);
        }

    }
}
