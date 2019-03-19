using Csvier.Abstract;
using Csvier.Exceptions;
using Csvier.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier
{
    class ParserHandler
    {
        private Type type;
        private CtorManager ctorManager;
        //private FieldParser fieldParser;
        //private PropertyParser propertyParser;
        private Dictionary<String, IParser> parsers;

        public ParserHandler(Type type)
        {
            this.type = type;
            ctorManager = new CtorManager(type);
            //fieldParser = new FieldParser(type);
            //propertyParser = new PropertyParser(type);
            parsers = new Dictionary<string, IParser>();
        }

        public void AddCtorParam(string name, int col)
        {
            ctorManager.AddCtorParameter(name, col);
        }

        public void AddProp(string name, int col)
        {
            //propertyParser.Add(name, col);
            if (parsers.ContainsKey(name))
                throw new PropertyException("allready constains a property with that name");
            parsers.Add(name, new PropertyParser(type, name, col));

        }

        public void AddField(string name, int col)
        {
            //fieldParser.Add(name, col);
            if (parsers.ContainsKey(name))
                throw new FieldException("allready constains a field with that name");
            parsers.Add(name, new FieldParser(type, name, col));
        }

    }
}
