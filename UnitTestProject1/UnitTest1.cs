using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using czas_pracy;
using Mb
using System.Data;





namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DodajPracownika()
        {
            DataAccess da = new DataAccess();
            string actualString = da.ConnectionString;
            string expectedString = System.Configuration.ConfigurationManager.ConnectionStrings[“DatabaseConnection”].ConnectionString;
            Assert.AreEqual(expectedString, actualString);
        }


        [Test]
        public void GetConnStringFromAppConfig()
        {
            DataAccess da = new DataAccess();
            string actualString = da.ConnectionString;
            string expectedString = System.Configuration.ConfigurationManager.ConnectionStrings[“DatabaseConnection”].ConnectionString;
            Assert.AreEqual(expectedString, actualString);
        }

    }
}
