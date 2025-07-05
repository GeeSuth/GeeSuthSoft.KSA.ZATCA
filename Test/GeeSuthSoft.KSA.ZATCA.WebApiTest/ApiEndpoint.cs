using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.WebApiTest;

public static class ApiEndpoint
{
    public static RouteGroupBuilder MapTestSimpleInvoiceApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/zatca-simple");

        group.WithTags("ZatcaSimple");

        group.MapGet("/share", async (IZatcaShareService shareZatcaService) =>
        {
            var result = await shareZatcaService.ShareInvoiceWithZatcaAsync(
                new Invoice(), true, new PCSIDInfoDto()
                );
            
            return Results.Ok(result);
        });

        return group;
    }
}