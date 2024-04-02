using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StableDiffusionCppLib;

public class SD : IDisposable
{
    private unsafe sd_ctx_t* ctx;
    private unsafe delegate* unmanaged[Cdecl]<uint, byte*, void*, void> sdLogCallback;

    public SD()
    {
        unsafe
        {
            this.sdLogCallback = &SdLogCallback;
            this.SetDefaultLogCallback();
        }
    }

    private void SetDefaultLogCallback()
    {
        unsafe
        {
            void* data = null;
            NativeMethods.sd_set_log_callback(this.sdLogCallback, data);
        }
    }

    public static int GetNumPhysicalCores()
    {
        return NativeMethods.get_num_physical_cores();
    }

    public static string GetSystemInfo()
    {
        unsafe
        {
            var systemInfo = NativeMethods.sd_get_system_info();
            return new string((sbyte*)systemInfo);
        }
    }

    public void Dispose()
    {
        unsafe
        {
            NativeMethods.free_sd_ctx(this.ctx);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe void SdLogCallback(uint level, byte* message, void* data)
    {
        Logger.LogCallback((SdLogLevel)level, NativeUtilities.to_string(message));
    }
}