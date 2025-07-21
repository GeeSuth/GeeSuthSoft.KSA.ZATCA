using System.Xml.Serialization;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Exceptions;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GeeSuthSoft.KSA.ZATCA.WebApiTest;

public static class ApiEndpoint
{
    public static RouteGroupBuilder MapTestSimpleInvoiceApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/zatca-simple");

        group.WithTags("ZatcaSimple");

        group.MapPost("/share-json", async (IZatcaShareService shareZatcaService, [FromBody] Invoice invoice) =>
        {
            var shareInvoiceRequestDto = new ShareInvoiceRequestDto()
            {
                invoiceObject = invoice,
                tokens = new PCSIDInfoDto()
                {
                    BinaryToken = "TUlJRDNqQ0NBNFNnQXdJQkFnSVRFUUFBT0FQRjkwQWpzL3hjWHdBQkFBQTRBekFLQmdncWhrak9QUVFEQWpCaU1SVXdFd1lLQ1pJbWlaUHlMR1FCR1JZRmJHOWpZV3d4RXpBUkJnb0praWFKay9Jc1pBRVpGZ05uYjNZeEZ6QVZCZ29Ka2lhSmsvSXNaQUVaRmdkbGVIUm5ZWHAwTVJzd0dRWURWUVFERXhKUVVscEZTVTVXVDBsRFJWTkRRVFF0UTBFd0hoY05NalF3TVRFeE1Ea3hPVE13V2hjTk1qa3dNVEE1TURreE9UTXdXakIxTVFzd0NRWURWUVFHRXdKVFFURW1NQ1FHQTFVRUNoTWRUV0Y0YVcxMWJTQlRjR1ZsWkNCVVpXTm9JRk4xY0hCc2VTQk1WRVF4RmpBVUJnTlZCQXNURFZKcGVXRmthQ0JDY21GdVkyZ3hKakFrQmdOVkJBTVRIVlJUVkMwNE9EWTBNekV4TkRVdE16azVPVGs1T1RrNU9UQXdNREF6TUZZd0VBWUhLb1pJemowQ0FRWUZLNEVFQUFvRFFnQUVvV0NLYTBTYTlGSUVyVE92MHVBa0MxVklLWHhVOW5QcHgydmxmNHloTWVqeThjMDJYSmJsRHE3dFB5ZG84bXEwYWhPTW1Obzhnd25pN1h0MUtUOVVlS09DQWdjd2dnSURNSUd0QmdOVkhSRUVnYVV3Z2FLa2daOHdnWnd4T3pBNUJnTlZCQVFNTWpFdFZGTlVmREl0VkZOVWZETXRaV1F5TW1ZeFpEZ3RaVFpoTWkweE1URTRMVGxpTlRndFpEbGhPR1l4TVdVME5EVm1NUjh3SFFZS0NaSW1pWlB5TEdRQkFRd1BNems1T1RrNU9UazVPVEF3TURBek1RMHdDd1lEVlFRTURBUXhNVEF3TVJFd0R3WURWUVFhREFoU1VsSkVNamt5T1RFYU1CZ0dBMVVFRHd3UlUzVndjR3g1SUdGamRHbDJhWFJwWlhNd0hRWURWUjBPQkJZRUZFWCtZdm1tdG5Zb0RmOUJHYktvN29jVEtZSzFNQjhHQTFVZEl3UVlNQmFBRkp2S3FxTHRtcXdza0lGelZ2cFAyUHhUKzlObk1Ic0dDQ3NHQVFVRkJ3RUJCRzh3YlRCckJnZ3JCZ0VGQlFjd0FvWmZhSFIwY0RvdkwyRnBZVFF1ZW1GMFkyRXVaMjkyTG5OaEwwTmxjblJGYm5KdmJHd3ZVRkphUlVsdWRtOXBZMlZUUTBFMExtVjRkR2RoZW5RdVoyOTJMbXh2WTJGc1gxQlNXa1ZKVGxaUFNVTkZVME5CTkMxRFFTZ3hLUzVqY25Rd0RnWURWUjBQQVFIL0JBUURBZ2VBTUR3R0NTc0dBUVFCZ2pjVkJ3UXZNQzBHSlNzR0FRUUJnamNWQ0lHR3FCMkUwUHNTaHUyZEpJZk8reG5Ud0ZWbWgvcWxaWVhaaEQ0Q0FXUUNBUkl3SFFZRFZSMGxCQll3RkFZSUt3WUJCUVVIQXdNR0NDc0dBUVVGQndNQ01DY0dDU3NHQVFRQmdqY1ZDZ1FhTUJnd0NnWUlLd1lCQlFVSEF3TXdDZ1lJS3dZQkJRVUhBd0l3Q2dZSUtvWkl6ajBFQXdJRFNBQXdSUUloQUxFL2ljaG1uV1hDVUtVYmNhM3ljaThvcXdhTHZGZEhWalFydmVJOXVxQWJBaUE5aEM0TThqZ01CQURQU3ptZDJ1aVBKQTZnS1IzTEUwM1U3NWVxYkMvclhBPT0=",
                    PCSIDSecret = "CkYsEXfV8c1gFHAtFWoZv73pGMvh/Qyo4LzKM2h/8Hg=",
                    privateKey = "MHQCAQEEID9fs3X0Q0n5gS0O6QkFXuJJKKxPhqOijb52/e1SKtdQoAcGBSuBBAAKoUQDQgAEGnvNFhqNaY/SxGORepyLwtSf274y2D0AsA+Kt3k21q2OSupfYeUot2Zf7dY30Fnq8n/fTljyFiaJAW5g5r0TLw=="
                },
                IsClearance = true
            };
            var result = await shareZatcaService.ShareInvoiceWithZatcaAsync(shareInvoiceRequestDto);
            return Results.Ok(result);
        });
        
        
        // This not working cause the web api minimal not support XML formatter nativly. 
        group.MapPost("/share-xml-deperecated", async (IZatcaShareService shareZatcaService, 
            [FromQuery] string binaryToken,
            [FromQuery] string pcsidSecret,
            [FromQuery] string privateKey,
            [FromBody] Invoice invoice) =>
        {
            ShareInvoiceRequestDto request = new ShareInvoiceRequestDto()
            {
                invoiceObject = invoice,
                tokens = new PCSIDInfoDto()
                {
                    BinaryToken = binaryToken,
                    PCSIDSecret = pcsidSecret,
                    privateKey = privateKey
                },
                IsClearance = true
            };
            
            var result = await shareZatcaService.ShareInvoiceWithZatcaAsync(request);
            return Results.Ok(result);
        }).Accepts<Invoice>("text/xml");

        
        group.MapPost("/share-xml", async (HttpContext context, 
            IZatcaShareService shareZatcaService, 
            [FromQuery] string binaryToken,
            [FromQuery] string pcsidSecret,
            [FromQuery] string privateKey) =>
        {
            if (context.Request.ContentType != "application/xml")
            {
                return Results.StatusCode(415); // Unsupported Media Type
            }

            using var reader = new StreamReader(context.Request.Body);
            var xmlContent = await reader.ReadToEndAsync();
            var serializer = new XmlSerializer(typeof(Invoice));
            using var stringReader = new StringReader(xmlContent);
            var invoice = (Invoice)serializer.Deserialize(stringReader);
            
            ShareInvoiceRequestDto request = new ShareInvoiceRequestDto()
            {
                invoiceObject = invoice,
                tokens = new PCSIDInfoDto()
                {
                    BinaryToken = binaryToken,
                    PCSIDSecret = pcsidSecret,
                    privateKey = privateKey
                },
                IsClearance = true
            };
            
            var result = await shareZatcaService.ShareInvoiceWithZatcaAsync(request);
            return Results.Ok(result);
            
        });
        
        return group;
    }
    
    
    public static RouteGroupBuilder MapTestOnBoarding(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/zatca-onboarding");

        group.WithTags("ZatcaOnboarding");

        group.MapPost("/Generate-Csr", async (IZatcaOnboardingService _onboardingService, CsrGenerationDto dto) =>
        {
            var Csr = _onboardingService.GenerateCsr(dto);
            return Results.Ok(Csr);
        });

        group.MapGet("/Get-CSID/{crs}", async (IZatcaOnboardingService _onboardingService, string crs) =>
        {
            var result = await _onboardingService.GetCSIDAsync(crs);
            return Results.Ok(result);
        });
        
        
        group.MapPost("/Get-PCSID", async (IZatcaOnboardingService _onboardingService, PCSIDRequestDto pcsidRequestDto) =>
        {
            var result = await _onboardingService.GetPCSIDAsync(pcsidRequestDto);
            return Results.Ok(result);
        });
        
        return group;
    }
    
    public static RouteGroupBuilder MapTestComplaince(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/zatca-sign");

        group.WithTags("ZatcaSign");

        group.MapPost("/Zatca-invoice-sign", 
            (IZatcaSignInvoiceService _zatcaSignInvoiceService , SignedInvoiceRequestDto invoice) =>
        {
            var result = _zatcaSignInvoiceService.GetSignedInvoice(invoice);
            return Results.Ok(result);
        });

      
        return group;
    }
}