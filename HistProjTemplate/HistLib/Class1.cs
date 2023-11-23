using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
    //Пока что оставляю все публичным. С грамотной инкапсуляцией разберемся при рефакторинге.
    public class Sect   //Класс раздел. Каждому разделу соответствует лист тестов по нему. (возможно с разными картами)
    {
        public string Name { get; set; }    //название раздела
        public List<Test> Tests;            //лист для хранения тестов
        public Sect(string s)               //конструктор для создания раздела по названию
        {
            Name = s;
            Tests = new List<Test>();
        }
        public Sect(string s, List<Test> lt)//конструктор для создания раздела по названию и листу тестов - для десериализованного варианта
        {
            Name = s;
            Tests = lt;
        }
        public void AddTest(Test t)         //функция добавления теста в раздел (возможно не нужна)
        {
            Tests.Add(t);
        }
    }
    public class Test   //класс тест. каждому тесту соответствует карта (через сурс), и массив вопрос-ответ = тест
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
        private List<string> AnswerOptions; //Подумать нужно ли иметь несколько вариантов ответа на вопрос или достаточно игнорировать регистр
        public Answer(List<string> ls)      //Если возможных ответов несколько использовать этот конструктор
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
        public Answer(string answer)    //Если вариант ответа один, использовать этот конструктор
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
