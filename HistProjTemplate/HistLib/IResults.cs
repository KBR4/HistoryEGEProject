namespace HistLib
{
    public interface IResults   //интерфейс выдачи результатов теста по заданным ответам
    {
        Statistics GetResults(string[] UserAnswers);
    }
}
