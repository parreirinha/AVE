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
        private FieldParser fieldParser;
        private PropertyParser propertyParser;


        public ParserHandler(Type type)
        {
            this.type = type;
            ctorManager = new CtorManager(type);
            fieldParser = new FieldParser(type);
            propertyParser = new PropertyParser(type);
        }

        public void AddCtorParam(string name, int col)
        {
            ctorManager.AddCtorParameter(name, col);
        }

        public void AddProp(string name, int col)
        {
            propertyParser.Add(name, col);
        }

        public void AddField(string name, int col)
        {
            fieldParser.Add(name, col);
        }

    }
}
