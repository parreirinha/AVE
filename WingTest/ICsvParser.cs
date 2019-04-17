using Clima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Test
{
    public interface ICsvParser
    {

        CsvParser Load(String src);

        object[] Parse();
    }
}
