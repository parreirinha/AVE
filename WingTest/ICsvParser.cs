using Clima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Test
{
    public interface ICsvParser<T>
    {

        CsvParser<T> Load(String src);

        IEnumerable<T> Parse();
    }
}
