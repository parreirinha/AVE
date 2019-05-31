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


        public StringDataLineEnumerable(string src)
        {
            this.src = src;
        }

        public IEnumerator<char> GetEnumerator()  //generica
        {
            return src.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() //chama generico
        {
            return GetEnumerator();
        }
    }

}
