using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GeeSuthSoft.KSA.ZATCA;

namespace GeeSuthSoft.KSA.ZATCA.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        //مؤسسة سالم ناصر احمد
        string vName = "مؤسسة سالم ناصر احمد";
        string vTax = "300056289500003";
        decimal vVatAmount = 15;
        decimal vTotalAmount = 100;

        [TestMethod]
        public void TestGetBase64()
        {
            var test = Qr.Qr.GetBase64(vName, vTax, DateTime.Now, vVatAmount, vTotalAmount, new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });

            TestContext.WriteLine(test);
            Assert.IsNotNull(test);
            

         
        }


        [TestMethod]
        public void TestGetBase64InUrl()
        {
            var test = Qr.Qr.GetBase64InUrl(vName, vTax, DateTime.Now, vVatAmount, vTotalAmount, new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });


            TestContext.WriteLine(test);
            Assert.IsNotNull(test);
            


        }


        [TestMethod]
        public void TestGetImage()
        {
            var test = Qr.Qr.GetImage(vName, vTax, DateTime.Now, decimal.Parse("186.45"), decimal.Parse("1429.4500"), new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });


            TestContext.WriteLine(test.Size.ToString());
            Assert.IsNotNull(test);
            

        }


    

    }
}
