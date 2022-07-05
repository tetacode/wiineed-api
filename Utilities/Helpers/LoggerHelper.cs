using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Utilities.Helpers;

public class LoggerHelper
{
    public static string CreateStackTraceMessageString(Exception e, object input = null)
    {
        if (input != null)
        {
            return JsonConvert.SerializeObject(new
            {
                Input = input,
                Error = $"{e.Message} -> {e.StackTrace}"
            });
        }
        return JsonConvert.SerializeObject(new
        {
            Error = $"{e.Message} -> {e.StackTrace}"
        });
    }
}