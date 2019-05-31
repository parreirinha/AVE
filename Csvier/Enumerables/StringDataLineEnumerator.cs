using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Enumerables
{
    public class StringDataLineEnumerator : IEnumerator<string>
    {
        private IEnumerator<char> data;
        EnumeratorOptions enumeratorOptions;
        private string curr;

        public StringDataLineEnumerator(IEnumerator<char> dataSourceStringEnumerator, EnumeratorOptions eo)
        {
            this.data = dataSourceStringEnumerator;
            this.enumeratorOptions = eo;
        }

        public string Current
        {
            get
            {
                return curr;
            }

            private set
            {
                curr = value;
            }   
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }


        public bool MoveNext()
        {
            string res = "";

            while(data.MoveNext())
            {
                if (data.Current == '\r')
                    continue;
                if(data.Current != '\n')
                {
                    res += data.Current;
                }
                else
                {
                    Current = res;
                    res = "";
                    return true;
                }                
            }
            if(res.Length > 0)
            {
                Current = res;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            data.Reset();
        }

        public void Dispose()
        {
            data.Dispose();
        }

        public bool Contains(string[] strArr, char c)
        {
            foreach(string s in strArr)
            {
                if (s.Contains(c))
                    return true;
            }
            return false;
        }
    }
}
