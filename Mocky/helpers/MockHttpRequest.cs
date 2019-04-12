using Mocky;
using Mocky.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MockIHttpRequest : MocksBase, IHttpRequest 
{


    public MockIHttpRequest(MockMethod[] ms) : base ( ms)
    {
        
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public string GetBody(string url)
    {
        throw new NotImplementedException();
    }
}
