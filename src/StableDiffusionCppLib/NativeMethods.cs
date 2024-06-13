// <copyright file="NativeMethods.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace StableDiffusionCppLib;

internal static unsafe partial class NativeMethods
{
    public delegate void SdLogCb(SdLogLevel level, string text, byte* data);
}