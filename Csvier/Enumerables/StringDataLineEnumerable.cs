using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Enumerables
{
    public class StringDataLineEnumerable : IEnumerable
    {

        string src;
        EnumeratorOptions options;


        public StringDataLineEnumerable(string src, EnumeratorOptions options)
        {
            this.src = src;
            this.options = options;
        }

        //public IEnumerator<char> GetEnumerator()  //generica
        //{
        //    return src.GetEnumerator();
        //}

        IEnumerator IEnumerable.GetEnumerator() //chama generico
        {
            return GetEnumerator();
        }

        public StringDataLineEnumerator GetEnumerator()
        {
            return new StringDataLineEnumerator(src.GetEnumerator(), options);
        }
    }

}
