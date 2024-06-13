// See https://aka.ms/new-console-template for more information

using StableDiffusionCppLib;
using StbImageWriteSharp;

Console.WriteLine(SD.GetSystemInfo());

Console.WriteLine(SD.GetNumPhysicalCores());

var modelPath = "/Users/drasticactions/Developer/sd_turbo.safetensors";

var sd = new SD();