using GeeSuthSoft.KSA.ZATCA.Dto;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Helper;

public abstract class LoggerHelper
{
    private readonly IZatcaApiConfig _zatcaApiConfig;
    private readonly ILogger _logger;

    protected LoggerHelper(IZatcaApiConfig zatcaApiConfig, ILogger logger)
    {
        _zatcaApiConfig = zatcaApiConfig;
        _logger = logger;
    }

    protected void LogZatcaInfo(string message)
    {
        if (_zatcaApiConfig.LogsEnabled)
        {
            _logger.LogInformation($"ZATCA INFO: {message}");
        }
    }
    
    
    protected void LogZatcaError(string message)
    {
        if (_zatcaApiConfig.LogsEnabled)
        {
            _logger.LogError($"ZATCA ERROR: {message}");
        }
    }
    
    protected void LogZatcaError(Exception ex, string? message = "")
    {
        if (_zatcaApiConfig.LogsEnabled)
        {
            _logger.LogError(ex, $"ZATCA ERROR: {message??""}");
        }
    }
    
    protected void LogZatcaWarning(string message)
    {
        if (_zatcaApiConfig.LogsEnabled)
        {
            _logger.LogWarning($"ZATCA WARN: {message}");
        }
    }
}