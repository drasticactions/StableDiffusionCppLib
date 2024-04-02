fn main() {
    bindgen::Builder::default()
        .header("../../external/stable-diffusion.cpp/stable-diffusion.h")
        .generate().unwrap()
        .write_to_file("stable-diffusion.rs").unwrap();

    csbindgen::Builder::default()
        .input_bindgen_file("stable-diffusion.rs")            // read from bindgen generated code
        .rust_file_header("use super::stablediffusion::*;")     // import bindgen generated modules(struct/method)
        .csharp_dll_name("stablediffusion")
        .csharp_namespace("StableDiffusionCppLib")
        .generate_to_file("stablediffusion_ffi.rs", "../../src/StableDiffusionCppLib/NativeMethods.g.cs")
        .unwrap();
}