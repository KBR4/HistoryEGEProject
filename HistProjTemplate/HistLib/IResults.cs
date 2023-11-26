using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
    public interface IResults   //интерфейс выдачи результатов теста по заданным ответам
    {
        Statistics GetResults(string[] UserAnswers);
    }
}
