using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Enumerables
{
    public class EnumeratorOptions
    {
        public bool isEven;
        public bool isOdd;
        public bool removeEmptys;
        public int skipFirst;
        public char[] spliterStrings;
        public string removeWith;
        public Predicate<string> pred;

        public EnumeratorOptions(bool isEven, bool isOdd, bool removeEmptys, int skipFirst, char[] spliterStrings, string removeWith, Predicate<string> pred)
        {
            this.isEven = isEven;
            this.isOdd = isOdd;
            this.removeEmptys = removeEmptys;
            this.skipFirst = skipFirst;
            this.spliterStrings = spliterStrings;
            this.removeWith = removeWith;
            this.pred = pred;
        }
    }
}
