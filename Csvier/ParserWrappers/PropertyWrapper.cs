using Csvier.Abstract;
using Csvier.Parsers;
using Csvier.Reflect;
using System;

namespace Csvier.Wrappers
{
    class PropertyWrapper : IParserWrapper
    {
        PropertyReflect pReflect;
        CsvBasicInfo csvInfo;

        public PropertyWrapper(PropertyReflect pReflect, CsvBasicInfo csvInfo)
        {
            this.pReflect = pReflect;
            this.csvInfo = csvInfo;
        }

        public void SetValue(string name, string[] lineData, int col, object target)
        {
            throw new NotImplementedException();
        }
    }
}
