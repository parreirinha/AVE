﻿using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.CsvAttributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class PropertyArgAttribute : Attribute, ICsvAttr
    {
        public string Name { get;  set; }
        public int Column { get;  set; }
        public MethodInfo mi { get;  set; }

        private Type type;

        public PropertyArgAttribute(Type type, string name, int col)
        {
            Name = name;
            Column = col;
            this.type = type;
            mi = type.GetMethod("PropArg", new Type[] { typeof(string), typeof(int)});
        }

        //// Invokes method PropArgs of CsvParser with the parameters name and col of constructor
        //public void InvokeMethodForCorrespondence(CsvParser<T> parser)
        //{
        //    object[] parameters = new object[] { Name, Column};
        //    mi.Invoke(parser, parameters);
        //}
    }
}
