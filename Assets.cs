using System;

namespace PAM;

public static class Assets
{
    
    public static readonly int TopColor = Shader.PropertyToID("_TopColor");
    public static readonly int MiddleColor = Shader.PropertyToID("_MiddleColor");
    public static readonly int BottomColor = Shader.PropertyToID("_BottomColor");


    public static Il2CppAssetBundle pam =
        Il2CppAssetBundleManager.LoadFromFile(@"E:\SlimeRancherModding\Unity\PAM2\Assets\AssetBundles\pam");//Il2CppAssetBundleManager.LoadFromMemory(LoadFromMemory("pam"));

    private static Byte[] LoadFromMemory(string name)
    {
        string streamName = $"PAM.{name}";
        var stream = EntryPoint.execAssembly.GetManifestResourceStream(streamName);
        if (stream == null) return null;
        byte[] ba = new byte[stream.Length];
                    
        var read = stream.Read(ba, 0, ba.Length);
        return ba;
    }


}