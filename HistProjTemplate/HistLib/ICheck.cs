using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistLib
{
    public interface ICheck //интерфейс проверки ответа
    {        
        bool Check(string s);
    }
}
