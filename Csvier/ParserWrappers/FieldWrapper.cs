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

        public void SetValue(string name, string[] lineData, int col, object target)
        {
            throw new NotImplementedException();
        }
    }
}
