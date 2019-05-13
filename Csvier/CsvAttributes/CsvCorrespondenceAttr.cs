using Csvier.Abstract;
using Csvier.CsvAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier
{
    public static class CsvCorrespondenceAttr<T>
    {
        /**
         * This static method will call all the methods(CtorArgs, PropArgs, FieldArgs) that are keeped in all attributes defined
         * this method gets all attributes of the object custom and for all attributes of type ICsvCustomAttributes 
         * calls InvokeMethodForCorrespondence
         * 
         * */
        public static void MakeAttributeCorrespondence(CsvParser<T> parser, Type custom)
        {
            object[] attrs = custom.GetCustomAttributes(false);

            foreach(object attr in attrs)
            {
                if (attr is ICsvAttr)   //not sure if this isthe best way
                {
                    ICsvAttr att = (ICsvAttr)attr;
                    //att.InvokeMethodForCorrespondence(parser);
                    MakeAttrCorrespondence(att, parser);
                }
            }
        }

        static void MakeAttrCorrespondence(ICsvAttr att, CsvParser<T> parser)
        {
            object[] parameters = new object[] { att.Name, att.Column };
            MethodInfo mi = att.mi;
            mi.Invoke(parser, parameters);
        }
    }
}
