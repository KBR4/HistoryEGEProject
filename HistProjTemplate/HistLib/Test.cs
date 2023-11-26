using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
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
            Source = source;            //TO DO: проверка подгрузки картинки
            AllQuestionsAnswers = QA;
        }
        public override string ToString()
        {
            return Name;
        }

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
