namespace StableDiffusionCppLib;

internal static unsafe partial class NativeMethods
{
    public delegate void SdLogCb(SdLogLevel level, string text, byte* data);
}