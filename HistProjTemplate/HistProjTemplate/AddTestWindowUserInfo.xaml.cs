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
using System.Text.RegularExpressions;
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
        private bool MapAdded;
        private bool QAdded;
        private List<string> UsedNames;
        public AddTestWindowUserInfo()
        {
            InitializeComponent();
            MapAdded = false;
            QAdded = false;
            NextB.IsEnabled = false;
            PrevB.IsEnabled = false;
            UsedNames = new List<string>();
        }
        public AddTestWindowUserInfo(List<string> lsn)
        {
            InitializeComponent();
            MapAdded = false;
            QAdded = false;
            NextB.IsEnabled = false;
            PrevB.IsEnabled = false;
            if (lsn!= null && lsn.Count > 0)
            {
                UsedNames = lsn;
            }
            else
            {
                UsedNames = new List<string>();
            }          
        }

        private void AddMapClick(object sender, RoutedEventArgs e) //добавить картинку, отправить ее в имагес, показать превью на экране
        {
            string checksource = GetImageSource(TextBoxName.Text);           
            //MessageBox.Show(ImageSource);
            if (checksource != null)
            {
                ImageSource = checksource;
                PreviewIMG.Source = new BitmapImage(new Uri(ImageSource, UriKind.RelativeOrAbsolute));
                PreviewIMG.Visibility = Visibility.Visible;
                MapAdded = true;
            }
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
            return null;  // априори обычно здесь нет ошибки
                              // и пользователь выбирает какую-то картинку, либо можно вернуть null
        }

        private void AddTXTFileClick(object sender, RoutedEventArgs e) //добавить тхт файл, сгенерировать из него массив QuestionsAnswers
        {
            List<QuestionAnswer> lqa = new List<QuestionAnswer>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл с вопросами и ответами для теста";

            //фильтр для того, чтобы выбирать только .txt
            dialog.Filter = "TXT files|*.txt;|All files|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                string pathToText = dialog.FileName;
                using (StreamReader sr = new StreamReader(pathToText))
                {
                    string[] sQ = sr.ReadToEnd().Split(new string[] { "Q:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (sQ.Length > 0)
                    {
                        for (int i = 0; i<sQ.Length; i++)
                        {                      
                            string[] sAns = sQ[i].Split(new string[] { "A:" }, StringSplitOptions.RemoveEmptyEntries);
                            if (sAns.Length > 1)
                            {
                                if (!string.IsNullOrEmpty(sAns[0].Trim(new char[] { ' ', '\n', '\t', '\r' })) && !string.IsNullOrEmpty(sAns[1].Trim(new char[] { ' ', '\n', '\t', '\r' })))
                                {
                                    string sq = sAns[0].Trim(new char[] { ' ', '\n', '\t', '\r' });
                                    Question q = new Question(sq);
                                    List<string> la = new List<string>();
                                    for (int j = 1; j < sAns.Length; j++)
                                    {
                                        string ss = sAns[j];
                                        ss = ss.Trim(new char[] { ' ', '\n', '\t', '\r' });
                                        if (!string.IsNullOrEmpty(ss))
                                        {
                                            la.Add(ss);
                                        }                                     
                                    }
                                    Answer ans = new Answer(la);
                                    QuestionAnswer qa = new QuestionAnswer(q, ans);
                                    lqa.Add(qa);
                                }
                                else
                                {
                                    MessageBox.Show("Проверьте соответствие используемого файла требованиям оформления - один из вопросов или ответов пуст");
                                    break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Проверьте соответствие используемого файла требованиям оформления - на один из вопрос не найдено ответов");
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Проверьте соответсвие используемого файла требованиям оформления - вопросы не найдены");
                    }
                }

            }
            questionAnswers = lqa;
            counter = 0;
            if (questionAnswers != null && questionAnswers.Count != 0)
            {
                if (questionAnswers.Count > 1)
                {
                    NextB.IsEnabled = true;
                }
                else
                {
                    NextB.IsEnabled = false;
                }
                PrevB.IsEnabled = false;
                TextBlockQuestion.Text = "Вопрос: " + questionAnswers[0].question.ToString() + "\n"+ "Ответ: " + questionAnswers[0].answer.GetAnswer();
            }
            else
            {
                TextBlockQuestion.Text = "Вопросов нет";
            }
            QAdded = true;
        }

        

        private void NextQClick(object sender, RoutedEventArgs e) //к следующему вопросу (вопросы подгружены из тхт)
        {
            counter++;
            if (questionAnswers.Count > 0 && counter < questionAnswers.Count)
            {
                if (counter == questionAnswers.Count - 1)
                {
                    NextB.IsEnabled = false;                   
                }
                PrevB.IsEnabled = true;
                TextBlockQuestion.Text = "Вопрос: " + questionAnswers[counter].question.ToString() + "\n" + "Ответ: " + questionAnswers[counter].answer.GetAnswer();
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
                if (counter == 0)
                {
                    PrevB.IsEnabled = false;
                }
                NextB.IsEnabled = true;
                TextBlockQuestion.Text = "Вопрос: " + questionAnswers[counter].question.ToString() + "\n" + "Ответ: " + questionAnswers[counter].answer.GetAnswer();
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
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название");
            }
            else
            {
                string Name = TextBoxName.Text;
                if (!UsedNames.Contains(Name))
                {
                    if (QAdded == false)
                    {
                        MessageBox.Show("Добавьте файл с вопросами");
                    }
                    else
                    {
                        if (MapAdded == false)
                        {
                            MessageBox.Show("Добавьте карту к данному тексту");
                        }
                        else
                        {
                            //MessageBox.Show(ImageSource);
                            AddedTest = new Test(TextBoxName.Text, ImageSource, questionAnswers);
                            //MessageBox.Show(AddedTest.Name + "\n" + AddedTest.AllQuestionsAnswers[0].question + "\n"
                            //    + AddedTest.AllQuestionsAnswers[0].answer.ToString() + "\n" + AddedTest.Source);
                            this.DialogResult = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Тест с таким названием уже существует в данном разделе. Выберите другое название");
                }
            }           
        }

        private void GobBackClick(object sender, RoutedEventArgs e) //назад
        {
            this.DialogResult = false;
        }

    }
}
