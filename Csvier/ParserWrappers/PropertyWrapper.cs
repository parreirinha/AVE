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

        public CsvBasicInfo GetBasicInfo()
        {
            return csvInfo;
        }

        public void SetValue(string name, string[] lineData, object target)
        {
            pReflect.SetValue(name, lineData[csvInfo.Column], target);
        }
    }
}
