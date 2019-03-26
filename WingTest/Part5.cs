using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClimateDataAPI;
using System.Collections.Generic;


namespace Csvier.Test
{
    [TestClass]
    public class Part5
    {
        [TestMethod]
        public void TestLoadSearchOporto()
        {
            using (ClimateDataApi api = new ClimateDataApi())
            {
                ClimateValues[] vals = api.GetClimateValues(ClimateDataApi.VALUES.PR, 1980, 1999);
                Assert.AreEqual(15, vals.Length);

                Assert.AreEqual("bccr_bcm2_0", vals[0].GCM);
                Assert.AreEqual("variableType", vals[0].variableType);
                Assert.AreEqual("jan", vals[0].Jan);
                Assert.AreEqual("feb", vals[0].Feb);
                Assert.AreEqual("mar", vals[0].Mar);

                Assert.AreEqual("cccma_cgcm3_1", vals[1].GCM);
                Assert.AreEqual("cnrm_cm3", vals[2].GCM);
                Assert.AreEqual("csiro_mk3_5", vals[3].GCM);
                Assert.AreEqual("gfdl_cm2_0", vals[4].GCM);
                Assert.AreEqual("gfdl_cm2_1", vals[5].GCM);
                Assert.AreEqual("ingv_echam4", vals[6].GCM);
                Assert.AreEqual("inmcm3_0", vals[7].GCM);
                Assert.AreEqual("ipsl_cm4", vals[8].GCM);
                Assert.AreEqual("miroc3_2_medres", vals[9].GCM);
                Assert.AreEqual("miub_echo_g", vals[10].GCM);
                Assert.AreEqual("mpi_echam5", vals[11].GCM);
                Assert.AreEqual("mri_cgcm2_3_2a", vals[12].GCM);
                Assert.AreEqual("ukmo_hadcm3", vals[13].GCM);
                Assert.AreEqual("ukmo_hadgem1", vals[14].GCM);
            }
        }
    }
}
