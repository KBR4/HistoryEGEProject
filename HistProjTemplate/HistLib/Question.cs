using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
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
}
