using Csvier.Abstract;
using Csvier.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Wrappers
{
    class FieldWrapper : IWrapper
    {

        FieldParser fp;
        ParserBasicInfo pbi;


        public FieldWrapper(FieldParser fp, ParserBasicInfo pbi)
        {
            this.fp = fp;
            this.pbi = pbi;
        }



        public void AddParser()
        {
            throw new NotImplementedException();
        }

        public void SetValue(string name, string[] lineData, int col, object target)
        {
            throw new NotImplementedException();
        }
    }
}
