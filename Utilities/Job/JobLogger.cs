using Microsoft.Extensions.Logging;

namespace Utilities.Job;

public class JobLogger
{
    private static ILoggerFactory _loggerFactory;
    private static ILogger<JobApplication> _logger;

    public static void LogInfo(string eventId, string message)
    {
        _logger.LogInformation($"Job Information({eventId}): {message}");
    }

    public static void LogError(string eventId, string message)
    {
        _logger.LogError($"Job Error({eventId}): {message}");
    }

    public static void LogWarning(string eventId, string message)
    {
        _logger.LogWarning($"Job Warning({eventId}): {message}");
    }

    public static void LogDebug(string eventId, string message)
    {
        _logger.LogDebug($"Job Debug({eventId}): {message}");
    }

    public static void LogCritical(string eventId, string message)
    {
        _logger.LogCritical($"Job Critical({eventId}): {message}");
    }
    
    public static void InitConsoleLogger(string pathFormat)
    {
        
        var path = pathFormat ?? $"Logs/log-{{Date}}-{DateTime.Now.ToString("HHmmss")}.txt";
        
        if (_loggerFactory == null)
        {
            _loggerFactory = LoggerFactory.Create(builder =>
                builder
                    .AddFile(path, LogLevel.Debug)
                    .AddSystemdConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.TimestampFormat = "HH:mm:ss ";
                    }));

            _logger = _loggerFactory.CreateLogger<JobApplication>();
            
            //System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        }
    }

    // static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    // {
    //     _logger.LogError(e.ExceptionObject.ToString());
    // }
}