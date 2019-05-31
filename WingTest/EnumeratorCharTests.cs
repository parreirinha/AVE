using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClimateDataAPI;
using System.Collections.Generic;
using Csvier.Enumerables;
using System.Diagnostics;
using System.Collections;

namespace Csvier.GenericTest
{
    [TestClass]
    public class EnumeratorCharTests
    {

        private string src =
@"2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56
2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56
2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png,Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54
2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";
        private string[] results =
        {
            "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56",
            "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56",
            "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png,Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54",
            "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55"
        };
        private EnumeratorOptions eo;
        private StringDataLineEnumerator strDataLineEnumerator;


        [TestMethod]
        public void EnumeratorCharTest()
        {
            eo = new EnumeratorOptions(false, false, true, 0, new char[] { '\r', '\n' }, null, null);

            strDataLineEnumerator = new StringDataLineEnumerator(src.GetEnumerator(), eo);
            ArrayList list = new ArrayList();

            while (strDataLineEnumerator.MoveNext())
            {
                list.Add(strDataLineEnumerator.Current);
            }

            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0], results[0]);
            Assert.AreEqual(list[1], results[1]);
            Assert.AreEqual(list[2], results[2]);
            Assert.AreEqual(list[3], results[3]);
        }
    }
}
