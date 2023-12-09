using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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
using Microsoft.Win32;
using static System.Collections.Specialized.BitVector32;

namespace HistProjTemplate
{
    /// <summary>
    /// Interaction logic for AddTestWindowUserInfo.xaml
    /// </summary>
    public partial class AddTestWindowUserInfo : Window
    {
        public Test AddedTest;
        private string ImageSource;
        private List<QuestionAnswer> questionAnswers;
        private int counter;
        public AddTestWindowUserInfo()
        {
            InitializeComponent();
        }

        // Сериализация вроде не нужна
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

        //public void AddTestByUser(string NameTest, string SectName)
        //{
        //    string ImageSource = GetImageSource(NameTest);
        //}

        private void AddMapClick(object sender, RoutedEventArgs e) //добавить картинку, отправить ее в имагес, показать превью на экране
        {
            //Егор - сделать
            ImageSource = GetImageSource(TextBoxName.Text);
            //MessageBox.Show(ImageSource);
            PreviewIMG.Source = new BitmapImage(new Uri(ImageSource, UriKind.RelativeOrAbsolute));
            PreviewIMG.Visibility = Visibility.Visible;
        }
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
                string nameFile = dialog.SafeFileName;
                //MessageBox.Show(nameFile);  
                pathToImages = pathToImages.Remove(pathToImages.Length - 30);
                // тут изменения
                pathToImages += "Images\\" + nameFile/*NameTest*/;
                //MessageBox.Show($"File name: {dialog.FileName} \nYour path: \n{pathToImages}");
                if (!File.Exists(pathToImages))
                    File.Copy(dialog.FileName, pathToImages);

                return pathToImages;     // абсолютный путь до изображения 
            }
            return "ОШИБКА";  // априори обычно здесь нет ошибки
                              // и пользователь выбирает какую-то картинку, либо можно вернуть null
        }

        private void AddTXTFileClick(object sender, RoutedEventArgs e) //добавить тхт файл, сгенерировать из него массив QuestionsAnswers
        {
            //Егор - сделать
            questionAnswers = new List<QuestionAnswer>();
            questionAnswers = GetAllQuestionsAnswers();
            //MessageBox.Show(questionAnswers.Count.ToString());
            //MessageBox.Show(questionAnswers[0].question.ToString() + "\n"
            //    + questionAnswers[0].answer.GetAnswer());
            counter = 0;
            if (questionAnswers.Count != 0)
            {
                TextBlockQuestion.Text = questionAnswers[0].question.ToString();
            }
            else
            {
                TextBlockQuestion.Text = "Вопросов нет.";
            }
        }

        public List<QuestionAnswer> GetAllQuestionsAnswers()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл с вопросами и ответами для теста";

            //фильтр для того, чтобы выбирать только .txt
            dialog.Filter =
        "TXT files|*.txt;|All files|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                string pathToText = dialog.FileName;
                // чтение из файла
                using (StreamReader sr = new StreamReader(pathToText))
                {
                    int j = 0;   // счетчик
                    List<string> answerList = new List<string>();       // лист ответов
                    string strQuestion = "";                            // пустая строка, чтобы прога не ругалась на 115 строке
                    Question newQuestion = new Question(strQuestion);   // целый вопрос
                    Answer newAnswer = new Answer("");                  // целый ответ
                    QuestionAnswer newQuestionAnswer;                   // объединение ответа и вопроса в один объект 
                    List<QuestionAnswer> newQuestionAnswers = new List<QuestionAnswer>();

                    if (!sr.EndOfStream)
                    {
                        while (!sr.EndOfStream)
                        {
                            string curString = sr.ReadLine();
                            bool first = true;
                            while (!sr.EndOfStream && !curString.Contains("A: "))
                            {
                                if (first)
                                {
                                    strQuestion += curString.Substring(2);
                                    first = false;
                                }
                                else
                                {
                                    curString = sr.ReadLine();
                                    if (curString.Contains("A: "))
                                    {
                                        newQuestion = new Question(strQuestion);
                                        strQuestion = "";
                                        //MessageBox.Show(curString.Substring(2));
                                        answerList.Add(curString.Substring(2));
                                        newAnswer = new Answer(curString.Substring(2));
                                        newQuestionAnswer = new QuestionAnswer(newQuestion, newAnswer);
                                        newQuestionAnswers.Add(newQuestionAnswer);
                                    }
                                    else
                                    {
                                        strQuestion += "\n" + curString;
                                    }

                                }

                            }

                            //MessageBox.Show($"{strQuestion}");
                            //strQuestion = "";
                        }
                        return newQuestionAnswers;
                        //MessageBox.Show(newQuestionAnswers[0].answer.ToString());
                    }
                    else
                    {
                        // нам дали пустой файл :(
                    }
                }
            }
            return null;  // априори обычно здесь нет ошибки, но на всякий случай null
        }

        private void NextQClick(object sender, RoutedEventArgs e) //к следующему вопросу (вопросы подгружены из тхт)
        {
            counter++;
            if (questionAnswers.Count > 0 && counter < questionAnswers.Count)
            {
                TextBlockQuestion.Text = questionAnswers[counter].question.ToString();
            }
            else
            {
                if (questionAnswers.Count == 0)
                {
                    TextBlockQuestion.Text = "Вопросов нет.";
                }
                else
                {
                    counter--;
                }
            }
        }

        private void PrevQClick(object sender, RoutedEventArgs e) //к предыдущему вопросу (вопросы подгружены из тхт)
        {
            counter--;
            if (questionAnswers.Count > 0 && counter >= 0)
            {
                TextBlockQuestion.Text = questionAnswers[counter].question.ToString();
            }
            else
            {
                if (questionAnswers.Count == 0)
                {
                    TextBlockQuestion.Text = "Вопросов нет.";
                }
                else
                {
                    counter++;
                }
            }
        }

        private void AddTestClick(object sender, RoutedEventArgs e) //кнопка возврата к предыдущему окну
        {
            //Егор - сделать
            AddedTest = new Test(TextBoxName.Text, ImageSource, questionAnswers);
            // AddedTest = ... //эта штука возвращает добавленный тест предыдущему окну
            this.DialogResult = true;
        }

        private void GobBackClick(object sender, RoutedEventArgs e) //назад
        {
            this.DialogResult = false;
        }
    }
}
