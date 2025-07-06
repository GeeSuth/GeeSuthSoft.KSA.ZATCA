namespace GeeSuthSoft.KSA.ZATCA.WebApiTest;

public static class ApiEndpointOnBoarding
{
    public static RouteGroupBuilder MapOnBoarding(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("OnBoarding");
        
        groupRoute.WithTags("ZatcaSimpleOnBoarding");
        
        //groupRoute.MapPost("GetCrs", )

        return groupRoute;
    }
}