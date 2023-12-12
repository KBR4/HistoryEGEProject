using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace HistLib
{
    [Serializable]
    public class Test : IResults   //класс тест. каждому тесту соответствует карта (через сурс), и массив вопрос-ответ = тест
    {
        public string Name { get; set; }        //Название теста (тема)
        public string Source { get; set; }      //Источник для изображения

        public List<QuestionAnswer> AllQuestionsAnswers; //Лист вопросов - ответов

        public Test(String name)        //Создание темы по названию
        {
            Name = name;
        }
        //Конструктор ниже будет использоваться для создания тестов десериализованных из файлов
        public Test(String name, string source, List<QuestionAnswer> QA)    //Создание темы по названию, изображению, массиву вопросов-ответов
        {
            Name = name;
            Source = source;
            AllQuestionsAnswers = QA;
        }
        public override string ToString()
        {
            return Name;
        }

        //TO DO: либо исправить исходную функцию, либо перенести куда-нибудь эти
        public static bool CheckUploadImage(string source)
        {
            if (!File.Exists(source) && CheckImageExpansion(source))
            {
                Console.WriteLine("Файл найден");
                return true;
            }
            else
            {
                if (!File.Exists(source))
                {
                    //Файл не существует
                    return false;
                }
                else
                {
                    if (CheckImageExpansion(source))
                    {
                        //Ошибка в ссылке
                        return false;
                    }
                    else
                    {
                        //Егор, ты косяк!
                        return false;
                    }
                }
            }
        }
        // найдем в пути, является ли файл .png, .jpg и тд 
        public static bool CheckImageExpansion(string source)
        {
            Regex regex = new Regex(@"[^\s]+(?=\.(jpg|gif|png|jpeg))\.");
            Match match = regex.Match(source);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Функция проверки теста - единственная работа класса Test
        public Statistics GetResults(string[] UserAnswers)
        {
            Statistics TestStats = new Statistics(this.AllQuestionsAnswers.Count());
            for (int i = 0; i<this.AllQuestionsAnswers.Count(); i++)
            {
                Answer CorrectAnswer = this.AllQuestionsAnswers[i].answer;
                if (this.AllQuestionsAnswers[i].Points == 1)    //Для вопросов с выбором 1 ответа
                {
                    if (CorrectAnswer.Check(UserAnswers[i]))
                    {
                        TestStats.CorrectAnswers++;
                        TestStats.CorrectAnswerNumbers[i] = 1;
                    }
                }
                else  //для вопросов с выбором ответа из нескольких утверждений (только для ответов формата цифрацифрацифра)
                {
                    if (!string.IsNullOrEmpty(UserAnswers[i]))
                    {
                        string Ans = CorrectAnswer.GetAnswer();
                        int len = Ans.Length;
                        int cnt = 0;
                        int cntwrong = 0;
                        foreach (char c in UserAnswers[i])
                        {
                            if (Ans.Contains(c))
                            {
                                cnt++;
                            }
                            else
                            {
                                cntwrong++;
                            }
                        }
                        if (cntwrong == 1)
                        {
                            if (cnt == len || cnt == len - 1)
                            {
                                TestStats.CorrectAnswers++;
                                TestStats.CorrectAnswerNumbers[i] = 1;
                            }
                        }
                        else
                        {
                            if (cntwrong == 0)
                            {
                                if (cnt == len)
                                {
                                    TestStats.CorrectAnswers++;
                                    TestStats.CorrectAnswerNumbers[i] = 2;
                                }
                                if (cnt == len - 1)
                                {
                                    TestStats.CorrectAnswers++;
                                    TestStats.CorrectAnswerNumbers[i] = 1;
                                }
                            }
                        }
                    }
                }
            }
            return TestStats;
        }
    }
}
