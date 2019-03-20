using Csvier.Abstract;
using Csvier.Parsers;
using Csvier.Reflect;
using System;

namespace Csvier.Wrappers
{
    class FieldWrapper : IParserWrapper
    {

        private FieldReflect fReflect;
        private CsvBasicInfo csvInfo;

        public FieldWrapper(FieldReflect fReflect, CsvBasicInfo csvInfo)
        {
            this.fReflect = fReflect;
            this.csvInfo = csvInfo;
        }

        public CsvBasicInfo GetBasicInfo()
        {
            return csvInfo;
        }

        public void SetValue(string[] lineData, object target)
        {
            fReflect.SetValue(csvInfo.climaType, csvInfo.Name, lineData[csvInfo.Column], target);
        }


    }
}
