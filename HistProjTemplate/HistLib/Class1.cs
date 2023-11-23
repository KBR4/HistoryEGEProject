using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
    public class Sect   //класс раздел. каждому разделу соответствует карта (через сурс), и массив вопрос-ответ = тест
    {
        public string Name { get; set; }
        public string Source { get; set; }

        public List<QuestionAnswer> AllQuestionsAnswers;

        public Sect(String name)
        {
            Name = name;
        }
        public Sect(String name, string source, List<QuestionAnswer> QA)
        {
            Name = name;
            Source = source;            //TO DO: проверка подгрузки картинки
            AllQuestionsAnswers = QA;
        }
    }
    //public class Map //кажется что этот класс не нужен
    //{
    //    public Image Img { get; set; }
    //    public Map(Image img)
    //    {
    //        Img = img;
    //    }
    //}
    public class Question               //вопрос = строка. проверка на слишком длинный вопрос
    {
        private string question;
        public string QuestionP
        {
            get
            {
                return question;
            }
            set
            {
                if (value.Length > 500)
                {
                    //MessageBox.Show("Вопрос слишком большой! Сократите длину вопроса до 500 символов.");
                }
                else
                {
                    question = value;
                }
            }
        }
        public Question(string s)
        {
            QuestionP = s;
        }
    }
    public class Answer                         //ответ - хранит информацию о всех принимающихся ответах на вопрос
    {
        private List<string> AnswerOptions;
        public Answer(List<string> ls)
        {
            AnswerOptions = new List<string>();
            if (ls.Count > 0)
            {
                AnswerOptions = ls;
            }
            else
            {
                //MessageBox.Show("Введите ответы!");
            }
        }
        public Answer(string answer)
        {
            AnswerOptions = new List<string>();
            AnswerOptions.Add(answer);
        }
        public bool CheckAnswer(string s) //проверка ответа - это нужно убрать в интерфейс
        {
            if (AnswerOptions.Contains(s))
            {
                return true;
            }
            return false;
        }
    }
    public class QuestionAnswer //отвечает за вопрос - ответ на экране, чтобы не путаться с тем, какому вопросу какой ответ соотвтетсвует.
                                //возможно поменять потом эту структуру
    {
        public Question question { get; set; }
        public Answer answer { get; set; }
        public QuestionAnswer(Question q, Answer a)
        {
            question = q;
            answer = a;
        }
    }
    public class Statistics //класс статистики - хранит информацию о количестве вопросов в данном тесте, число правильных и номер текущего вопроса
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Counter { get; set; }
        public Statistics(int t)
        {
            TotalQuestions = t;
            CorrectAnswers = 0;
            Counter = 0;
        }
    }
}
