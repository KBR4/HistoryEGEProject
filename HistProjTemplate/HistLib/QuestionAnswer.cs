using System;

namespace HistLib
{
    [Serializable]
    public class QuestionAnswer //отвечает за вопрос - ответ на экране, чтобы не путаться с тем, какому вопросу какой ответ соотвтетсвует.
    {
        public int Points { get; set; }
        public Question question { get; set; }
        public Answer answer { get; set; }
        public QuestionAnswer(Question q, Answer a, int p)
        {
            Points = p;
            question = q;
            answer = a;
        }
    }
}
