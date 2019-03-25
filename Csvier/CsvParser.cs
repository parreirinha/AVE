﻿using Csvier.CsvAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Csvier
{

    public class CsvParser
    {
        private Type type;
        private char separator;
        private ParserHandler parser;
        private string[] data;

        public CsvParser(Type klass, char separator)
        {
            type = klass;
            this.separator = separator;
            parser = new ParserHandler(type, separator);
        }
        public CsvParser(Type klass) : this(klass, ',')
        {
        }

        public CsvParser CtorArg(string arg, int col)
        {
            parser.AddCtorParam(arg, col);
            return this;
        }

        public CsvParser PropArg(string arg, int col)
        {
            parser.AddProp(arg, col);
            return this;
        }

        public CsvParser FieldArg(string arg, int col)
        {
            parser.AddField(arg, col);
            return this;
        }

        public CsvParser Load(String src)
        {
            string[] arr = src.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            data = arr;
            return this;
        }

        public CsvParser Remove(int count)
        {
            data = data.Skip(count).ToArray();
            return this;
        }

        public CsvParser RemoveEmpties()
        {
            data = data.Where(item => item != "").ToArray();
            return this;
        }
        
        public CsvParser RemoveWith(string word)
        {
            data = data.Where(item => !item.Contains(word)).ToArray();
            return this;
        }
        public CsvParser RemoveEvenIndexes()
        {
            data = data.Where((item, index) => index % 2 == 0).ToArray();
            return this;
        }
        public CsvParser RemoveOddIndexes()
        {
            data = data.Where((item, index) => index % 2 != 0).ToArray();
            return this;
        }
        public object[] Parse()
        {
            object[] weathers= parser.GetObjects(data);

            for (int i = 0; i < weathers.Length; i++)
                parser.SetFieldAndPropertiesValues(weathers[i], data[i]);

            return weathers;
        }
    }
}
