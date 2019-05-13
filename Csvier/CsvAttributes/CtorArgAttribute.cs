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
        public string Name { get; set; }
        public int Column { get; set; }
        public MethodInfo mi { get; set; }

        private Type type;

        public CtorArgAttribute(Type parserType, string name, int col)
        {
            Name = name;
            Column = col;
            this.type = parserType;
            mi = parserType.GetMethod("CtorArg", new Type[] { typeof(string), typeof(int) });
        }

        //// Invokes method CtorArgs of CsvParser with the parameters name and col of constructor
        //public void InvokeMethodForCorrespondence(CsvParser<T> parser)
        //{
        //    object[] parameters = new object[] { Name, Column };
        //    mi.Invoke(parser, parameters);
        //}
    }
}
