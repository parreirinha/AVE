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
        public static void MakeAttributeCorrespondence(CsvParser parser, Type custom)
        {
            Type type = parser.GetType();
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
