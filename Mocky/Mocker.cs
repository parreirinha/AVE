using Mocky.Emiters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Mocky
{
    public class Mocker
    {
        private readonly Type klass;
        private Dictionary<string, MockMethod> ms;

        public Mocker(Type klass)
        {
            this.klass = klass;
            this.ms = new Dictionary<string, MockMethod>();
        }

        public MockMethod When(string name)
        {
            MockMethod m;
            if (!ms.TryGetValue(name, out m))
            {
                m = new MockMethod(klass, name);
                ms.Add(name, m);
            }
            return m;
        }

        public object Create()
        {
            Type t = BuildType();
            return Activator.CreateInstance(t, new object[1] { ms.Values.ToArray() });
        }

        private Type BuildType()
        {
            // !!!!!! TO DO !!!!!!
            //throw new NotImplementedException();

            SimpleEmiter emiter = new SimpleEmiter(klass, "DynamicAssembly");
            emiter.BuildConstructorWithOneParameter();
            emiter.BuildMethods();
            Type createdType = emiter.CreateType();

            return createdType;
        }

        private Type[] GetParamTypes(ParameterInfo[] pi)
        {
            Type[] res = new Type[pi.Length];

            int idx = 0;
            foreach(ParameterInfo p in pi)
            {
                res[idx++] = p.ParameterType;
            }

            return res;
        }
    }
}
