using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Request;

namespace Mocky.Test
{
    [TestClass]
    public class TestMockerForPartialHttpRequest
    {
        readonly IHttpRequest req;

        public TestMockerForPartialHttpRequest()
        {
            Mocker mock = new Mocker(typeof(IHttpRequest));

            mock.When("GetBody").With("www.somerequest.com").Return("response");
            mock.When("GetBody").With("www.anotherrequest.com").Return("anotherresponse");
            req = (IHttpRequest)mock.Create();
        }

        [TestMethod]
        public void TestHttpRequestSuccessfully()
        {
            Assert.AreEqual(req.GetBody("www.somerequest.com"), "response");
            Assert.AreEqual(req.GetBody("www.anotherrequest.com"), "anotherresponse");
            Assert.AreEqual(req.GetBody("www.notDifinedRequest.com"), 0); //when returns 0 the cast throws an exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestHttpRequestFailing()
        {
            req.Dispose(); // NotImplementedException
        }

    }
}

