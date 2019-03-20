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

        public void SetValue(string[] lineData, object target)
        {
            pReflect.SetValue(csvInfo.climaType, csvInfo.Name, lineData[csvInfo.Column], target);
        }
    }
}
