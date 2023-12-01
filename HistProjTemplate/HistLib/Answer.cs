using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
    [Serializable]
    public class Answer : ICheck                         //ответ - хранит информацию о всех принимающихся ответах на вопрос
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
        public string GetAnswer()
        {
            return AnswerOptions[0];
        }

        public bool Check(string s)
        {
            if (AnswerOptions.Contains(s))
            {
                return true;
            }
            return false;
        }
    }
}
