using Csvier.CsvAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Csvier
{

    public class CsvParser<T>
    {
        private Type type;
        private char separator;
        private ParserHandler<T> parser;
        private string[] data;

        public CsvParser(char separator)
        {
            type = typeof(T);
            this.separator = separator;
            parser = new ParserHandler<T>(separator);
        }
        public CsvParser() : this(',')
        {
        }

        public CsvParser<T> CtorArg(string arg, int col)
        {
            parser.AddCtorParam(arg, col);
            return this;
        }

        public CsvParser<T> PropArg(string arg, int col)
        {
            parser.AddProp(arg, col);
            return this;
        }

        public CsvParser<T> FieldArg(string arg, int col)
        {
            parser.AddField(arg, col);
            return this;
        }

        public CsvParser<T> Load(String src)
        {
            string[] arr = src.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            data = arr;
            return this;
        }

        public CsvParser<T> Remove(int count)
        {
            data = data.Skip(count).ToArray();
            return this;
        }

        public CsvParser<T> RemoveEmpties()
        {
            data = data.Where(item => item != "").ToArray();
            return this;
        }
        
        public CsvParser<T> RemoveWith(string word)
        {
            data = data.Where(item => !item.Contains(word)).ToArray();
            return this;
        }
        public CsvParser<T> RemoveEvenIndexes()
        {
            data = data.Where((item, index) => index % 2 == 0).ToArray();
            return this;
        }
        public CsvParser<T> RemoveOddIndexes()
        {
            data = data.Where((item, index) => index % 2 != 0).ToArray();
            return this;
        }
        public T[] Parse()
        {
            IEnumerable<T> weathers = parser.GetObjects(data);     // creates objects

            int idx = 0;
            foreach(T elem in weathers)
                parser.SetFieldAndPropertiesValues(elem, data[idx++]);

            return weathers.ToArray();
        }

        public T[] Parse(Func<string, T> parser)
        {
            List<T> aux = new List<T>();
            T currObj;

            foreach(string line in data)
            {
                currObj = parser(line);
                aux.Add(currObj);
            }

            return aux.ToArray();
        }
    }
}
