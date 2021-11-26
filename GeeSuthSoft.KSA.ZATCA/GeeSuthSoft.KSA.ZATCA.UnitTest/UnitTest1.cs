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

        [TestMethod]
        public void TestGetBase64()
        {
            var test = Qr.QrCodeGenerate.GetBase64(vName, "1201010101110", DateTime.Now, decimal.Parse("186.45"), decimal.Parse("1429.4500"), new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });

            TestContext.WriteLine(test);
            Assert.IsNotNull(test);
            

         
        }


        [TestMethod]
        public void TestGetBase64InUrl()
        {
            var test = Qr.QrCodeGenerate.GetBase64InUrl(vName, "120101010111012", DateTime.Now, decimal.Parse("186.45"), decimal.Parse("1429.45"), new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });


            TestContext.WriteLine(test);
            Assert.IsNotNull(test);
            


        }


        [TestMethod]
        public void TestGetImage()
        {
            var test = Qr.QrCodeGenerate.GetImage(vName, "1201010101110", DateTime.Now, decimal.Parse("186.45"), decimal.Parse("1429.4500"), new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });


            TestContext.WriteLine(test.Size.ToString());
            Assert.IsNotNull(test);
            

        }


        [TestMethod]
        public void TestTLV()
        {
            var test = Qr.QrCodeGenerate.GetTLVString(vName, "123456789123456", DateTime.Now, 15, 115, new Qr.QrCodeOption() { Language = Enums.Options.Language.Ar });

            TestContext.WriteLine(test);
            Assert.IsNotNull(test);


        }

    }
}
