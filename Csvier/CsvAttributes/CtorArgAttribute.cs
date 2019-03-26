using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.CsvAttributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class CtorArgAttribute : Attribute, ICsvAttr
    {
        private string Name { get; set; }
        private int Column { get; set; }
        private MethodInfo mi;
        private Type type;

        public CtorArgAttribute(Type parserType, string name, int col)
        {
            Name = name;
            Column = col;
            this.type = parserType;
            mi = parserType.GetMethod("CtorArg", new Type[] { typeof(string), typeof(int) });
        }

        // Invokes method CtorArgs of CsvParser with the parameters name and col of constructor
        public void InvokeMethodForCorrespondence(CsvParser parser)
        {
            object[] parameters = new object[] { Name, Column };
            mi.Invoke(parser, parameters);
        }
    }
}
