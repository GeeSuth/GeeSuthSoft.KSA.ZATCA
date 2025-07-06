using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Helper;

public abstract class LoggerHelper(IZatcaApiConfig zatcaApiConfig, ILogger logger)
{
    protected void LogZatcaInfo(string message)
    {
        if (zatcaApiConfig.LogsEnabled)
        {
            logger.LogInformation($"ZATCA INFO: {message}");
        }
    }
    
    
    protected void LogZatcaError(string message)
    {
        if (zatcaApiConfig.LogsEnabled)
        {
            logger.LogError($"ZATCA ERROR: {message}");
        }
    }
    
    protected void LogZatcaError(Exception ex, string? message = "")
    {
        if (zatcaApiConfig.LogsEnabled)
        {
            logger.LogError(ex, $"ZATCA ERROR: {message??""}");
        }
    }
    
    protected void LogZatcaWarning(string message)
    {
        if (zatcaApiConfig.LogsEnabled)
        {
            logger.LogWarning($"ZATCA WARN: {message}");
        }
    }
}