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

            //Проверка существование картинки по источнику (пути)
            if (CheckUploadImage(source))
            {
                Source = source;
            }
            else
            {
                source = null; //TO DO: добавить пустую картинку чтобы здесь не было пусто
            }
            AllQuestionsAnswers = QA;
        }
        public override string ToString()
        {
            return Name;
        }

        //TO DO: либо исправить исходную функцию, либо перенести куда-нибудь эти
        //Это вспомогательные функции, поэтому они не должны лежать в данном классе (мне так кажется)
        //Встроить логику в оригинальный код (я вроде сделал, проверь Егор)
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
                if (CorrectAnswer.Check(UserAnswers[i]))
                {
                    TestStats.CorrectAnswers++;
                    TestStats.CorrectAnswerNumbers[i] = 1;
                }
            }
            return TestStats;
        }
    }
}
