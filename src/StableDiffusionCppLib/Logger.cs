namespace StableDiffusionCppLib;

public static class Logger
{
    public static bool Verbose { get; set; } = true;

    public static void LogCallback(SdLogLevel level, string message)
    {
        if (!Verbose && level <= SdLogLevel.SdLogDebug)
        {
            return;
        }

        if (level <= SdLogLevel.SdLogDebug)
        {
            Console.Out.WriteLine(message);
        }
        else
        {
            Console.Error.WriteLine(message);
        }
    }
}