// <copyright file="NativeUtilities.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Runtime.InteropServices;
using System.Text;

namespace StableDiffusionCppLib;

internal static unsafe class NativeUtilities
{
    public static byte* to_bytep(this string? str)
    {
        if (str == null)
        {
            return (byte*)0;
        }

        int bufferSize = LengthSize(str);
        byte *buffer = (byte*)NativeMemory.Alloc((nuint)bufferSize);
        fixed (char* strPtr = str)
        {
            Encoding.UTF8.GetBytes(strPtr, str.Length + 1, buffer, bufferSize);
        }

        return buffer;
    }

    public static string to_string(byte *s, bool freePtr = false)
    {
        if (s == null)
        {
            return string.Empty;
        }

        byte* ptr = (byte*) s;
        while (*ptr != 0)
        {
            ptr++;
        }

        string result = System.Text.Encoding.UTF8.GetString(
            s, (int) (ptr - (byte*) s)
        );

        if (freePtr)
        {
            NativeMemory.Free(s);
        }
        return result;
    }

    public static int LengthSize(string? str)
    {
        if (str == null)
        {
            return 0;
        }

        return (str.Length * 4) + 1;
    }

    public static unsafe void FreePointer(byte* ptr)
    {
        if (ptr != null)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
    }
}

public enum SdLogLevel
{
    SdLogDebug,
    SdLogInfo,
    SdLogWarn,
    SdLogError
}

public enum SdType
{
    SdTypeF32 = 0,
    SdTypeF16 = 1,
    SdTypeQ40 = 2,
    SdTypeQ41 = 3,

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