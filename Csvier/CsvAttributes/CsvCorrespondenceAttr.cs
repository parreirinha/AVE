using Csvier.Abstract;
using Csvier.CsvAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier
{
    public static class CsvCorrespondenceAttr
    {
        /**
         * This static method will call all the methods(CtorArgs, PropArgs, FieldArgs) that are keeped in all attributes defined
         * this method gets all attributes of the object custom and for all attributes of type ICsvCustomAttributes 
         * calls InvokeMethodForCorrespondence
         * 
         * */
        public static void MakeAttributeCorrespondence(CsvParser parser, Type custom)
        {
            object[] attrs = custom.GetCustomAttributes(false);

            foreach(object attr in attrs)
            {
                if (attr is ICsvAttr)   //not sure if this isthe best way
                {
                    ICsvAttr att = (ICsvAttr)attr;
                    att.InvokeMethodForCorrespondence(parser);
                }
            }
        }
    }
}
