using System.Runtime.InteropServices;

namespace StableDiffusionCppLib;

public static unsafe class Interop
{
    private const string DllName = "stable-diffusion"; 
    
    public enum SdLogLevel
    {
        SdLogDebug,
        SdLogInfo,
        SdLogWarn,
        SdLogError
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdImage
    {
        public uint Width;
        public uint Height;
        public uint Channel;
        public IntPtr Data; // Pointer to image data
    }

    // Delegate for log callback
    public delegate void SdLogCb(SdLogLevel level, string text, IntPtr data);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr sd_type_name(SdType type);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void sd_set_log_callback(SdLogCb logCallback, IntPtr data);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int get_num_physical_cores();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr sd_get_system_info();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr new_sd_ctx(
        string modelPath,
        string vaePath,
        string taesdPath,
        string controlNetPath,
        string loraModelDir,
        string embedDir,
        [MarshalAs(UnmanagedType.Bool)] bool vaeDecodeOnly,
        [MarshalAs(UnmanagedType.Bool)] bool vaeTiling,
        [MarshalAs(UnmanagedType.Bool)] bool freeParamsImmediately,
        int nThreads,
        SdType wType,
        RngType rngType,
        Schedule s,
        [MarshalAs(UnmanagedType.Bool)] bool keepControlNetCpu);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void free_sd_ctx(IntPtr ctx);
    
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr txt2img(IntPtr sdCtx,
                                        string prompt,
                                        string negativePrompt,
                                        int clipSkip,
                                        float cfgScale,
                                        int width,
                                        int height,
                                        SampleMethod sampleMethod,
                                        int sampleSteps,
                                        long seed,
                                        int batchCount,
                                        IntPtr controlCond,
                                        float controlStrength);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr img2img(IntPtr sdCtx,
                                        SdImage initImage,
                                        string prompt,
                                        string negativePrompt,
                                        int clipSkip,
                                        float cfgScale,
                                        int width,
                                        int height,
                                        SampleMethod sampleMethod,
                                        int sampleSteps,
                                        float strength,
                                        long seed,
                                        int batchCount);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr img2vid(IntPtr sdCtx,
                                        SdImage initImage,
                                        int width,
                                        int height,
                                        int videoFrames,
                                        int motionBucketId,
                                        int fps,
                                        float augmentationLevel,
                                        float minCfg,
                                        float cfgScale,
                                        SampleMethod sampleMethod,
                                        int sampleSteps,
                                        float strength,
                                        long seed);

    [StructLayout(LayoutKind.Sequential)]
    public struct UpscalerCtx
    {
    }

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr new_upscaler_ctx(string esrganPath,
                                                 int nThreads,
                                                 SdType wType);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void free_upscaler_ctx(IntPtr upscalerCtx);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SdImage upscale(IntPtr upscalerCtx,
                                         SdImage inputImage,
                                         uint upscaleFactor);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool convert(string inputPath,
                                      string vaePath,
                                      string outputPath,
                                      SdType outputType);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr preprocess_canny(IntPtr img,
                                                  int width,
                                                  int height,
                                                  float highThreshold,
                                                  float lowThreshold,
                                                  float weak,
                                                  float strong,
                                                  [MarshalAs(UnmanagedType.Bool)] bool inverse);
}

public enum SdType
{
    SdTypeF32 = 0,
    SdTypeF16 = 1,
    SdTypeQ40 = 2,
    SdTypeQ41 = 3,
    // Skipped removed types as they do not have values in the enum
    SdTypeQ50 = 6,
    SdTypeQ51 = 7,
    SdTypeQ80 = 8,
    SdTypeQ81 = 9,
    SdTypeQ2K = 10,
    SdTypeQ3K = 11,
    SdTypeQ4K = 12,
    SdTypeQ5K = 13,
    SdTypeQ6K = 14,
    SdTypeQ8K = 15,
    SdTypeIQ2XXS = 16,
    SdTypeIQ2XS = 17,
    SdTypeIQ3XXS = 18,
    SdTypeIQ1S = 19,
    SdTypeIQ4NL = 20,
    SdTypeI8,
    SdTypeI16,
    SdTypeI32,
    SdTypeCount // This should always be the last element to indicate the count of types
}

public enum RngType
{
    StdDefaultRng,
    CudaRng
}

public enum SampleMethod
{
    EulerA,
    Euler,
    Heun,
    Dpm2,
    Dpmpp2SA,
    Dpmpp2M,
    Dpmpp2Mv2,
    Lcm,
    NSampleMethods
}

public enum Schedule
{
    Default,
    Discrete,
    Karras,
    NSchedules
}

public enum SDMode
{
    TXT2IMG,
    IMG2IMG,
    IMG2VID,
    CONVERT,
    MODE_COUNT
}