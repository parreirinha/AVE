using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Parsers
{
    public class CsvBasicInfo
    {
        public CsvBasicInfo(Type type, string name, int column, char separator)
        {
            Name = name;
            Column = column;
            Separator = separator;
            climaType = type;
        }

        public string Name { get; }
        public int Column { get; }
        public char Separator { get; }
        public Type climaType { get; }
    }
}
