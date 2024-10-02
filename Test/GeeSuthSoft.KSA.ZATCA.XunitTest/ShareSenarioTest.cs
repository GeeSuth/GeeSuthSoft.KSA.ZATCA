using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class ShareSenarioTest : IClassFixture<ServiceProviderFixture>
    {

        // Basic Information suppose those saved inside database after onboarding process, the info is result of onboarding procces
        string PCSIDBinaryToken = "TUlJRDNqQ0NBNFNnQXdJQkFnSVRFUUFBT0FQRjkwQWpzL3hjWHdBQkFBQTRBekFLQmdncWhrak9QUVFEQWpCaU1SVXdFd1lLQ1pJbWlaUHlMR1FCR1JZRmJHOWpZV3d4RXpBUkJnb0praWFKay9Jc1pBRVpGZ05uYjNZeEZ6QVZCZ29Ka2lhSmsvSXNaQUVaRmdkbGVIUm5ZWHAwTVJzd0dRWURWUVFERXhKUVVscEZTVTVXVDBsRFJWTkRRVFF0UTBFd0hoY05NalF3TVRFeE1Ea3hPVE13V2hjTk1qa3dNVEE1TURreE9UTXdXakIxTVFzd0NRWURWUVFHRXdKVFFURW1NQ1FHQTFVRUNoTWRUV0Y0YVcxMWJTQlRjR1ZsWkNCVVpXTm9JRk4xY0hCc2VTQk1WRVF4RmpBVUJnTlZCQXNURFZKcGVXRmthQ0JDY21GdVkyZ3hKakFrQmdOVkJBTVRIVlJUVkMwNE9EWTBNekV4TkRVdE16azVPVGs1T1RrNU9UQXdNREF6TUZZd0VBWUhLb1pJemowQ0FRWUZLNEVFQUFvRFFnQUVvV0NLYTBTYTlGSUVyVE92MHVBa0MxVklLWHhVOW5QcHgydmxmNHloTWVqeThjMDJYSmJsRHE3dFB5ZG84bXEwYWhPTW1Obzhnd25pN1h0MUtUOVVlS09DQWdjd2dnSURNSUd0QmdOVkhSRUVnYVV3Z2FLa2daOHdnWnd4T3pBNUJnTlZCQVFNTWpFdFZGTlVmREl0VkZOVWZETXRaV1F5TW1ZeFpEZ3RaVFpoTWkweE1URTRMVGxpTlRndFpEbGhPR1l4TVdVME5EVm1NUjh3SFFZS0NaSW1pWlB5TEdRQkFRd1BNems1T1RrNU9UazVPVEF3TURBek1RMHdDd1lEVlFRTURBUXhNVEF3TVJFd0R3WURWUVFhREFoU1VsSkVNamt5T1RFYU1CZ0dBMVVFRHd3UlUzVndjR3g1SUdGamRHbDJhWFJwWlhNd0hRWURWUjBPQkJZRUZFWCtZdm1tdG5Zb0RmOUJHYktvN29jVEtZSzFNQjhHQTFVZEl3UVlNQmFBRkp2S3FxTHRtcXdza0lGelZ2cFAyUHhUKzlObk1Ic0dDQ3NHQVFVRkJ3RUJCRzh3YlRCckJnZ3JCZ0VGQlFjd0FvWmZhSFIwY0RvdkwyRnBZVFF1ZW1GMFkyRXVaMjkyTG5OaEwwTmxjblJGYm5KdmJHd3ZVRkphUlVsdWRtOXBZMlZUUTBFMExtVjRkR2RoZW5RdVoyOTJMbXh2WTJGc1gxQlNXa1ZKVGxaUFNVTkZVME5CTkMxRFFTZ3hLUzVqY25Rd0RnWURWUjBQQVFIL0JBUURBZ2VBTUR3R0NTc0dBUVFCZ2pjVkJ3UXZNQzBHSlNzR0FRUUJnamNWQ0lHR3FCMkUwUHNTaHUyZEpJZk8reG5Ud0ZWbWgvcWxaWVhaaEQ0Q0FXUUNBUkl3SFFZRFZSMGxCQll3RkFZSUt3WUJCUVVIQXdNR0NDc0dBUVVGQndNQ01DY0dDU3NHQVFRQmdqY1ZDZ1FhTUJnd0NnWUlLd1lCQlFVSEF3TXdDZ1lJS3dZQkJRVUhBd0l3Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUxFL2ljaG1uV1hDVUtVYmNhM3ljaThvcXdhTHZGZEhWalFydmVJOXVxQWJBaUE5aEM0TThqZ01CQURQU3ptZDJ1aVBKQTZnS1IzTEUwM1U3NWVxYkMvclhBPT0=";
        string PCSIDSecret = "CkYsEXfV8c1gFHAtFWoZv73pGMvh/Qyo4LzKM2h/8Hg=";
        string CrsPrivateKey = "MHQCAQEEIMJ2AKQKHbLDJIkAE0AEemVDx2aMFvmZSnEsS8ytwEbwoAcGBSuBBAAKoUQDQgAEBLqcpoam/ZbB/ZCfTss+yCwbH1T79vVJ2hPrQcuxQhpG+fsuOwEhke8jDMLeZiXygStCmWnRxxrqjuGnrqd3pw==";
        //MHQCAQEEIM9SHyquNJsAHErVNe5jEgCkbBrg61PYPOJ3f4xVWe4boAcGBSuBBAAKoUQDQgAEzMzYv9A1eVyCoHwQdI6drqGC1Jfkf5n7xEEOIFjEeUBuSllhWhpGCH+7n5RaTFUzHqOFJkedEv1gqaGUA2F56A==
        
        private readonly IZatcaInvoiceService _zatcaInvoiceService;

        public ShareSenarioTest(ServiceProviderFixture fixture)
        {
            _zatcaInvoiceService = fixture.ServiceProvider.GetRequiredService<IZatcaInvoiceService>();
        }
        
        [Fact]
        public async Task ShareSignedInvoiceWithZatca()
        {
            // 1. Get Generate Invoice
            var invoiceObject = ConstValue.InvoicesTemplateTest.GetSimpleInvoice();
            if(invoiceObject == null ) Assert.Fail("Generate Invoice Is Failed!");


            // 2. Signed Invoice
            //SignInvoice signInvoice = new SignInvoice();

            //var signed = signInvoice.GenerateSignedInvoice(invoiceObject,
            //    BinaryToken: BinarySecurityToken,
            //    Secret: CrsPrivateKey);

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
            Encoding.UTF8.GetString(Convert.FromBase64String(PCSIDBinaryToken)),
            CrsPrivateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();

            if (signeed is null || signeed.RequestApi == null) Assert.Fail("Sign Invoice is Failed!");

            // 3. Check Compalice Invoice -- Seems not need in real senario.


            // 4. Share Invoice With Zatca

            var reportInvoiceZatca = await _zatcaInvoiceService.SendInvoiceToZatcaApi(signeed.RequestApi,
                PCSIDBinaryToken: PCSIDBinaryToken,
                PCSIDSecret: PCSIDSecret, false);


            Assert.NotNull(reportInvoiceZatca);

            Assert.True(reportInvoiceZatca.IsSuccessStatusCode);
        }
    }
}
