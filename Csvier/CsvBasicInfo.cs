using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Parsers
{
    /**
     *  This objects keeps the information related with de csv file and the type of parameter of the corresponding column
     * */
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
