using System.Runtime.InteropServices;

namespace StableDiffusionCppLib;

public class SD : IDisposable
{
    private IntPtr sdCtx;

    public SD(string modelPath, string vaePath, string taesdPath, string controlNetPath, string loraModelDir, string embedDir, bool vaeDecodeOnly, bool vaeTiling, bool freeParamsImmediately, int nThreads, SdType wType, RngType rngType, Schedule schedule, bool keepControlNetCpu)
    {
        this.sdCtx = Interop.new_sd_ctx(modelPath, vaePath, taesdPath, controlNetPath, loraModelDir, embedDir, vaeDecodeOnly, vaeTiling, freeParamsImmediately, nThreads, wType, rngType, schedule, keepControlNetCpu);
        if (this.sdCtx == IntPtr.Zero)
        {
            throw new Exception("Failed to create Stable Diffusion context");
        }
    }

    public Interop.SdImage Txt2Img(SDParams sdParams)
    {
        Interop.SdImage? img = null;
        var resultPtr = Interop.txt2img(this.sdCtx, sdParams.Prompt, sdParams.NegativePrompt, sdParams. ClipSkip, sdParams.CfgScale, sdParams.Width, sdParams.Height, sdParams.SampleMethod, sdParams.SampleSteps, sdParams.Seed, sdParams.BatchCount, IntPtr.Zero, sdParams.ControlStrength);
        return Marshal.PtrToStructure<Interop.SdImage>(resultPtr);
    }
    
    public static int GetNumPhysicalCores()
    {
        return Interop.get_num_physical_cores();
    }
    
    public static string GetSystemInfo()
    {
        IntPtr infoPtr = Interop.sd_get_system_info();
        
        if (infoPtr != IntPtr.Zero)
        {
            string? systemInfo = Marshal.PtrToStringAnsi(infoPtr);
            return systemInfo ?? string.Empty;
        }
        
        throw new Exception("Failed to get system information");
    }
    
    public static void SetLogCallback(Interop.SdLogCb logCallback, IntPtr data)
    {
        Interop.sd_set_log_callback(logCallback, data);
    }
    
    public static void SetDefaultLogCallback()
    {
        Interop.sd_set_log_callback(Logger.LogCallback, IntPtr.Zero);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.sdCtx != IntPtr.Zero)
        {
            Interop.free_sd_ctx(this.sdCtx);
            this.sdCtx = IntPtr.Zero;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

public static class Logger
{
    public static bool Verbose { get; set; } = true;

    public static void LogCallback(Interop.SdLogLevel level, string message, IntPtr data)
    {
        if (!Verbose && level <= Interop.SdLogLevel.SdLogDebug)
        {
            return;
        }

        if (level <= Interop.SdLogLevel.SdLogDebug)
        {
            Console.Out.WriteLine(message);
        }
        else
        {
            Console.Error.WriteLine(message);
        }
    }
}