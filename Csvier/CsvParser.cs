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


        public CsvParser(Type klass, char separator)
        {
            type = klass;
            this.separator = separator;
            parser = new ParserHandler(type);
        }
        public CsvParser(Type klass) : this(klass, ',')
        {
        }

        public CsvParser CtorArg(string arg, int col)
        {
            //Part 1
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
            ConstructorInfo[] ci = type.GetConstructors();
            Object[] ctorParams = GetConstructorParams(ci);
            if (ctorParams == null)
                throw new Exception("there is no constructor that matches de pameters identified in CtorArgs method");

            object created = Activator.CreateInstance(type, ctorParams);


            //Part 1
            throw new NotImplementedException();
        }

        /*
         * get object[] for Activator
         * if values don't match with ctor params returns null
         **/
        private object[] GetConstructorParams(ConstructorInfo[] ci)
        {
            // será necessáriovalidar o tipo de valores dos parametros para os iniciar
            throw new NotImplementedException();
        }
    }
}
