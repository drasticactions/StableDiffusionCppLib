namespace StableDiffusionCppLib;

public class SDParams
{
    /// <summary>
    /// Number of threads to use during computation. If less than or equal to 0, uses the number of CPU physical cores.
    /// </summary>
    public int NThreads { get; set; } = -1;

    /// <summary>
    /// Run mode: TXT2IMG, IMG2IMG, or CONVERT. Default is TXT2IMG.
    /// </summary>
    public SDMode Mode { get; set; } = SDMode.TXT2IMG;

    /// <summary>
    /// Path to the main model file.
    /// </summary>
    public string ModelPath { get; set; } = string.Empty;

    /// <summary>
    /// Path to VAE model file.
    /// </summary>
    public string VaePath { get; set; } = string.Empty;

    /// <summary>
    /// Path to TAESD. Using Tiny AutoEncoder for fast decoding (lower quality).
    /// </summary>
    public string TaesdPath { get; set; } = string.Empty;

    /// <summary>
    /// Path to ESRGAN model. Currently, only supports RealESRGAN_x4plus_anime_6B.
    /// </summary>
    public string EsrganPath { get; set; } = string.Empty;

    /// <summary>
    /// Path to control net model.
    /// </summary>
    public string ControlNetPath { get; set; } = string.Empty;

    /// <summary>
    /// Path to embeddings directory.
    /// </summary>
    public string EmbeddingsPath { get; set; } = string.Empty;

    /// <summary>
    /// Weight type (e.g., f32, f16). If not specified, defaults to the type of the weight file.
    /// </summary>
    public SdType WType { get; set; } = SdType.SdTypeCount;

    /// <summary>
    /// Directory for Lora model.
    /// </summary>
    public string LoraModelDir { get; set; } = string.Empty;

    /// <summary>
    /// Output path for the result image. Default is "./output.png".
    /// </summary>
    public string OutputPath { get; set; } = "output.png";

    /// <summary>
    /// Path to the input image, required for IMG2IMG mode.
    /// </summary>
    public string InputPath { get; set; } = string.Empty;

    /// <summary>
    /// Path to image condition for control net.
    /// </summary>
    public string ControlImagePath { get; set; } = string.Empty;

    /// <summary>
    /// The prompt to render.
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The negative prompt. Default is empty.
    /// </summary>
    public string NegativePrompt { get; set; } = string.Empty;

    /// <summary>
    /// Unconditional guidance scale. Default is 7.0.
    /// </summary>
    public float MinCfg { get; set; } = 1.0f;

    /// <summary>
    /// Scale for CFG. Default is 7.0.
    /// </summary>
    public float CfgScale { get; set; } = 7.0f;

    /// <summary>
    /// Number of CLIP network layers to skip. Default is -1, which is unspecified.
    /// </summary>
    public int ClipSkip { get; set; } = -1;

    /// <summary>
    /// Image width in pixels. Default is 512.
    /// </summary>
    public int Width { get; set; } = 512;

    /// <summary>
    /// Image height in pixels. Default is 512.
    /// </summary>
    public int Height { get; set; } = 512;

    /// <summary>
    /// Number of images to generate in one batch.
    /// </summary>
    public int BatchCount { get; set; } = 1;

    /// <summary>
    /// Number of video frames to generate, used in IMG2VID mode.
    /// </summary>
    public int VideoFrames { get; set; } = 6;

    /// <summary>
    /// Identifier for motion bucket, used in IMG2VID mode.
    /// </summary>
    public int MotionBucketId { get; set; } = 127;

    /// <summary>
    /// Frames per second for video, used in IMG2VID mode.
    /// </summary>
    public int Fps { get; set; } = 6;

    /// <summary>
    /// Level of augmentation. Default is 0.0.
    /// </summary>
    public float AugmentationLevel { get; set; } = 0.0f;

    /// <summary>
    /// Sampling method. Default is EULER_A.
    /// </summary>
    public SampleMethod SampleMethod { get; set; } = SampleMethod.EulerA;

    /// <summary>
    /// Denoiser sigma schedule. Default is DEFAULT.
    /// </summary>
    public Schedule Schedule { get; set; } = Schedule.Default;

    /// <summary>
    /// Number of sample steps. Default is 20.
    /// </summary>
    public int SampleSteps { get; set; } = 20;

    /// <summary>
    /// Strength for noising/unnoising. Default is 0.75.
    /// </summary>
    public float Strength { get; set; } = 0.75f;

    /// <summary>
    /// Strength to apply Control Net. Default is 0.9.
    /// </summary>
    public float ControlStrength { get; set; } = 0.9f;

    /// <summary>
    /// Type of RNG to use. Default is CUDA_RNG.
    /// </summary>
    public RngType RngType { get; set; } = RngType.CudaRng;

    /// <summary>
    /// RNG seed. Default is 42. Use a negative value for a random seed.
    /// </summary>
    public long Seed { get; set; } = 42;

    /// <summary>
    /// Run ESRGAN upscaler this many times. Default is 1.
    /// </summary>
    public int UpscaleRepeats { get; set; } = 1;

    /// <summary>
    /// If true, process VAE in tiles to reduce memory usage.
    /// </summary>
    public bool VaeTiling { get; set; } = false;

    /// <summary>
    /// If true, keep control net in CPU to save VRAM.
    /// </summary>
    public bool ControlNetCpu { get; set; } = false;

    /// <summary>
    /// If true, apply canny preprocessor for edge detection.
    /// </summary>
    public bool CannyPreprocess { get; set; } = false;

    /// <summary>
    /// If true, print extra information during processing.
    /// </summary>
    public bool Verbose { get; set; } = false;
}