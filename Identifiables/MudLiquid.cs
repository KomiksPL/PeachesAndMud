using System.Collections.Generic;
using DebuggingMod;
using Il2CppSystem;
using PAM.Utility;
using UnityEngine.Scripting;
using AppDomain = System.AppDomain;
using Object = UnityEngine.Object;

namespace PAM.Identifiables;

public static class MudLiquid
{
    public static List<Il2CppSystem.Object> colors = new List<Il2CppSystem.Object>();
    public static void Build()
    {
        var mudWater = LookupRegistry.GetIdentifiableByName("MudWater");
        GameObject waterPrefab = PrefabUtils.CopyPrefab(LookupRegistry.GetVanillaIdentifiableByName("Water").prefab);
        waterPrefab.name = "MudIdentifiable";
        waterPrefab.GetComponent<Identifiable>().identType = mudWater;
        mudWater.prefab = waterPrefab;
        DestroyOnTouching component1 = waterPrefab.GetComponent<DestroyOnTouching>();
        component1.destroyFX = PrefabUtils.CopyPrefab(component1.destroyFX);
        
        
         
      
        var module = component1.destroyFX.transform.Find("Hit").GetComponent<ParticleSystem>().main;
        
        //module.startColor = new ParticleSystem.MinMaxGradient(ColorUtils.FromHex("33200e"));
        Material material = Object.Instantiate(SRObjects.Get<Material>("Depth Water Ball"));
        material.SetTexture("_ColorRamp", TextureUtils.CreateRamp("33200e", "1a1108"));
        waterPrefab.transform.Find("Sphere").GetComponent<MeshRenderer>().sharedMaterial = material;
        var mainModule = waterPrefab.transform.Find("Sphere/FX Sprinkler 1").GetComponent<ParticleSystem>().main;

        waterPrefab.transform.Find("Sphere/FX Water Glops").GetComponent<ParticleSystemRenderer>().sharedMaterial = material;
        component1.destroyFX.transform.Find("Water Glops").GetComponent<ParticleSystemRenderer>().sharedMaterial = material;
    }
    public static void BuildForAutoSave()
    {
        var liquidDefinition1 = SRObjects.Get<LiquidDefinition>("Water");
        LiquidDefinition liquidDefinition = ScriptableObjectUtils.CreateScriptable<LiquidDefinition>(definition =>
        {
            definition.name = "MudWater";
            definition.isWater = true;
            definition.inFX = liquidDefinition1.inFX;
            definition.vacFailFX = liquidDefinition1.vacFailFX;
            definition.icon = liquidDefinition1.icon;
            definition.localizedName = TranslationPatcher.AddTranslation("Actor", "l.mud_water", "Mud");
            definition.color = Color.blue;
        });
        LookupRegistry.RegistryIdentifiable(liquidDefinition);
    }
}