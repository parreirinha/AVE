using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Csvier
{
    public class CsvParser
    {
        Type type;
        char separator;
        Dictionary<string, int> ctorArgs = new Dictionary<string, int>();

        public CsvParser(Type klass, char separator)
        {
            type = klass;
            this.separator = separator;
        }
        public CsvParser(Type klass) : this(klass, ',')
        {
        }

        public CsvParser CtorArg(string arg, int col)
        {
            //Part 1

            ctorArgs.Add(arg, col);
            return this;
        }

        public CsvParser PropArg(string arg, int col)
        {
            return this;
        }

        public CsvParser FieldArg(string arg, int col)
        {
            return this;
        }

        public CsvParser Load(String src)
        {
            //Part 1

            string[] arr = src.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return this;
        }

        public CsvParser Remove(int count)
        {
            return this;
        }

        public CsvParser RemoveEmpties()
        {
            return this;
        }
        
        public CsvParser RemoveWith(string word)
        {
            return this;
        }
        public CsvParser RemoveEvenIndexes()
        {
            return this;
        }
        public CsvParser RemoveOddIndexes()
        {
            return this;
        }
        public object[] Parse()
        {
            //Part 1
            throw new NotImplementedException();
        }

    }
}
