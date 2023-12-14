using System;

namespace HistLib
{
    [Serializable]
    public class Question               //вопрос = строка. проверка на слишком длинный вопрос
    {
        public string Que { get; set; }
        public Question(string s)
        {
            Que = s;
        }
        public override string ToString()
        {
            return Que;
        }
    }
}
