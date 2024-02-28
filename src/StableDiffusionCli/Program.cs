// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using StableDiffusionCppLib;
using StbImageWriteSharp;

Console.WriteLine("Hello, World!");

Console.WriteLine(SD.GetSystemInfo());

Console.WriteLine(SD.GetNumPhysicalCores());

var paramObject = new SDParams();
paramObject.Prompt = "a lovely cat";
paramObject.ModelPath = "/Users/drasticactions/Developer/Personal/sd_turbo.safetensors";

SD.SetDefaultLogCallback();

var vaeDecodeOnly = paramObject.Mode == SDMode.IMG2IMG || paramObject.Mode == SDMode.IMG2VID;

var sd = new SD(
    paramObject.ModelPath,
    paramObject.VaePath,
    paramObject.TaesdPath,
    paramObject.ControlNetPath,
    paramObject.LoraModelDir,
    paramObject.EmbeddingsPath,
    vaeDecodeOnly,
    paramObject.VaeTiling,
    true,
    paramObject.NThreads,
    paramObject.WType,
    paramObject.RngType,
    paramObject.Schedule,
    paramObject.ControlNetCpu
);

var result = sd.Txt2Img(paramObject);

using (Stream stream = File.OpenWrite("output.png"))
{
    int dataSize = (int)(result.Width * result.Height * result.Channel);
    
    byte[] managedArray = new byte[dataSize];
    Marshal.Copy(result.Data, managedArray, 0, dataSize);
    ImageWriter writer = new ImageWriter();
    writer.WritePng(managedArray, (int)result.Width, (int)result.Height, (StbImageWriteSharp.ColorComponents)result.Channel, stream);
}

sd.Dispose();

