// <copyright file="SD.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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

    public void Txt2Img(string prompt,
        string negativePrompt,
        int clipSkip,
        float cfgScale,
        int width,
        int height,
        uint sampleMethod,
        int sampleSteps,
        long seed,
        int batchCount,
        float controlStrength,
        float styleStrength,
        bool normalizeInput,
        string inputIdImagesPath)
        {
            unsafe
            {
                byte* promptPtr = prompt.to_bytep();
                byte* negativePromptPtr = negativePrompt.to_bytep();
                byte* inputIdImagesPathPtr = inputIdImagesPath.to_bytep();

                NativeMethods.txt2img(
                    this.ctx,
                    promptPtr,
                    negativePromptPtr,
                    clipSkip,
                    cfgScale,
                    width,
                    height,
                    sampleMethod,
                    sampleSteps,
                    seed,
                    batchCount,
                    null,
                    controlStrength,
                    styleStrength,
                    normalizeInput,
                    inputIdImagesPathPtr);

                NativeUtilities.FreePointer(promptPtr);
                NativeUtilities.FreePointer(negativePromptPtr);
                NativeUtilities.FreePointer(inputIdImagesPathPtr);

            }
        }

    public bool CreateSdContext(
        string modelPath,
        string vaePath,
        string taesdPath,
        string controlNetPath,
        string loraModelDir,
        string embedDir,
        string stackedIdEmbedDir,
        bool vaeDecodeOnly,
        bool vaeTiling,
        bool freeParamsImmediately,
        int nThreads,
        uint wtype,
        uint rngType,
        uint seed,
        bool keepClipOnCpu,
        bool keepControlNetCpu,
        bool keepVaeOnCpu)
    {
        unsafe
        {
            byte* modelPathPtr = modelPath.to_bytep();
            byte* vaePathPtr = vaePath.to_bytep();
            byte* taesdPathPtr = taesdPath.to_bytep();
            byte* controlNetPathPtr = controlNetPath.to_bytep();
            byte* loraModelDirPtr = loraModelDir.to_bytep();
            byte* embedDirPtr = embedDir.to_bytep();
            byte* stackedIdEmbedDirPtr = stackedIdEmbedDir.to_bytep();

            this.ctx = NativeMethods.new_sd_ctx(
            modelPathPtr,
            vaePathPtr,
            taesdPathPtr,
            controlNetPathPtr,
            loraModelDirPtr,
            embedDirPtr,
            stackedIdEmbedDirPtr,
            vaeDecodeOnly,
            vaeTiling,
            freeParamsImmediately,
            nThreads,
            wtype,
            rngType,
            seed,
            keepClipOnCpu,
            keepControlNetCpu,
            keepVaeOnCpu);

            NativeUtilities.FreePointer(modelPathPtr);
            NativeUtilities.FreePointer(vaePathPtr);
            NativeUtilities.FreePointer(taesdPathPtr);
            NativeUtilities.FreePointer(controlNetPathPtr);
            NativeUtilities.FreePointer(loraModelDirPtr);
            NativeUtilities.FreePointer(embedDirPtr);
            NativeUtilities.FreePointer(stackedIdEmbedDirPtr);

            return this.ctx != null;
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