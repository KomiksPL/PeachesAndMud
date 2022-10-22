using System.Collections.Generic;
using System.Linq;
using DebuggingMod;
using PAM.Utility;

namespace PAM.Identifiables.Food;

//[NoRegistry]
public class MuddyHen : IdentifiableTypeCreator
{
    public override string Name => "MuddyHen";
    public override void Build()
    {
        
        var birdMuddyChick = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("birdChick"));
        birdMuddyChick.GetComponent<Identifiable>().identType = identTypes[0];
        birdMuddyChick.name = "birdMuddyChick";
        identTypes[0].prefab = birdMuddyChick;
        SkinnedMeshRenderer mRenderChick = birdMuddyChick.transform.Find("Chickadoo/mesh_body1").gameObject.GetComponent<SkinnedMeshRenderer>();
        Material ChickMat = UnityEngine.Object.Instantiate<Material>(mRenderChick.sharedMaterial);
        var texture2D = TextureUtils.CreateRamp(ColorUtils.FromHex("33200e"), ColorUtils.FromHex("261001"));
        ChickMat.SetTexture("_RampBlue", texture2D);
        ChickMat.SetTexture("_RampBlack", texture2D);
        mRenderChick.sharedMaterial = ChickMat;
            
        foreach (MeshRenderer renderChick in birdMuddyChick.transform.Find("Chickadoo/root").GetComponentsInChildren<MeshRenderer>())
        {
            if (renderChick.sharedMaterial.name.Equals("Chickadoo"))
            {
                renderChick.sharedMaterial = ChickMat;
            }
        }
        var muddyHen = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("birdHen"));
        muddyHen.GetComponent<Identifiable>().identType = identTypes[1];
        muddyHen.name = "birdMuddyHen";
        identTypes[1].prefab = muddyHen;
        var transformAfterTime = birdMuddyChick.GetComponent<TransformAfterTime>();
        transformAfterTime.options.Clear();
        transformAfterTime.options.Add(new TransformAfterTime.TransformOpt
        {
            weight = 9,
            targetPrefab = muddyHen
        });
        transformAfterTime.options.Add(new TransformAfterTime.TransformOpt()
        {
            weight = 1,
            targetPrefab = SRObjects.Get<GameObject>("birdRooster")
        });

        SkinnedMeshRenderer mRender = muddyHen.transform.Find("Hen Hen/mesh_body1").gameObject.GetComponent<SkinnedMeshRenderer>();
        Material HenMat = UnityEngine.Object.Instantiate<Material>(mRender.sharedMaterial);
        HenMat.PrintContent();
        texture2D = TextureUtils.CreateRamp(ColorUtils.FromHex("33200e"), ColorUtils.FromHex("261001"));
        HenMat.SetTexture("_MaskMap", texture2D);
        //HenMat.SetTexture("_RampBlack", texture2D);
        mRender.sharedMaterial = HenMat;
        foreach (MeshRenderer renderHen in muddyHen.transform.Find("Hen Hen/root").GetComponentsInChildren<MeshRenderer>())
        {
            if (renderHen.sharedMaterial.name.Equals("HenHen"))
            {
                renderHen.sharedMaterial = HenMat;
            }
                
        }

        muddyHen.GetComponent<Reproduce>().childPrefab = birdMuddyChick;
    }

    public override IdentifiableType[] BuildIdentifiable()
    {
        IdentifiableType muddyHen = ScriptableObjectUtils.CreateScriptable<IdentifiableType>(definition =>
        {
            definition.name = Name;
            definition.referenceId = "IdentifiableType." + definition.name;
            definition.IsAnimal = true;
            definition.localizedName = TranslationPatcher.AddTranslation("Actor", "l.muddy_hen", "Muddy Hen");
            definition.foodGroup = SRObjects.Get<IdentifiableTypeGroup>("MeatGroup");
        });
        IdentifiableType birdMuddy = ScriptableObjectUtils.CreateScriptable<IdentifiableType>(definition =>
        {
            definition.name = "MuddyChick";
            definition.referenceId = "IdentifiableType." + definition.name;
            definition.IsAnimal = true;
            definition.localizedName = TranslationPatcher.AddTranslation("Actor", "l.muddy_chick", "Muddy Chickadoo");
            definition.foodGroup = SRObjects.Get<IdentifiableTypeGroup>("MeatGroup");
        });
        LookupRegistry.RegistryIdentifiable(muddyHen);
        LookupRegistry.RegistryIdentifiable(birdMuddy);
        LookupRegistry.RegistryVaccable(birdMuddy);
        LookupRegistry.RegistryVaccable(muddyHen);
        return new[] {birdMuddy, muddyHen };


    }
}