using System;
using System.Linq;
using DebuggingMod;
using Il2CppMonomiPark.SlimeRancher.Slime;
using PAM.Utility;
using Object = UnityEngine.Object;

namespace PAM.Identifiables.SlimesAndPlorts;

public class MudSlime : IdentifiableTypeCreator
{
	public override string Name => "Mud";

	public override void Build()
    {
        IdentifiableType mudPlort = identTypes[0];
        
        GameObject plortPrefab = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("plortPuddle"));
        plortPrefab.name = "plort" + Name;
        plortPrefab.GetComponent<Identifiable>().identType = mudPlort;
        plortPrefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
        Material material = Object.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(SRObjects.Get<IdentifiableType>("Puddle")).AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
        material.SetColor("_TopColor", new Color32(51, 32, 14, byte.MaxValue));
        material.SetColor("_MiddleColor", new Color32(38, 16, 1, 0));
        material.SetColor("_BottomColor", new Color32(26, 17, 8, byte.MaxValue));
        material.SetColor("_SpecColor", new Color32(38, 16, 1, 0));
        material.SetFloat("_Shininess", 1f);
        material.SetFloat("_Gloss", 1f);
        plortPrefab.GetComponent<MeshRenderer>().sharedMaterial = material;
        mudPlort.prefab = plortPrefab;
        PlortRegistry.RegisterPlort(mudPlort, 120, 1);
        SlimeDefinition mudSlime = identTypes[1].Cast<SlimeDefinition>();
	    GameObject slimeMud = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("slimePuddle"));
	    slimeMud.name = "slime" + Name;
	    
	    SlimeDefinition identifiableType = slimeMud.GetComponent<Identifiable>().identType.Cast<SlimeDefinition>();
	    mudSlime.Diet = identifiableType.Diet;
	    mudSlime.nativeZones = identifiableType.nativeZones;
	    mudSlime.Sounds = identifiableType.Sounds;
	    mudSlime.properties = identifiableType.properties;
	    SlimeRegistry.RegistrySlimeDefinition(identifiableType);
	    SlimeAppearance slimeAppearance = Object.Instantiate<SlimeAppearance>(identifiableType.AppearancesDefault[0]);
	    slimeAppearance.name = Name + "Default";
	    
	    Material material3 = Object.Instantiate<Material>(identifiableType.AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
	    material3.hideFlags |= HideFlags.HideAndDontSave;
	    material3.SetColor("_TopColor", new Color32(51, 32, 14, byte.MaxValue));
	    material3.SetColor("_MiddleColor", new Color32(38, 16, 1, 0));
	    material3.SetColor("_BottomColor", new Color32(26, 17, 8, byte.MaxValue));
	    material3.SetColor("_SpecColor", new Color32(38, 16, 1, 0));
	    material3.SetFloat("_Shininess", 1f);
	    material3.SetFloat("_Gloss", 1f);
	    slimeAppearance.Structures[0].DefaultMaterials[0] = material3;
			    
	    foreach (SlimeExpressionFace expressionFace in slimeAppearance.Face.ExpressionFaces)
	    {
		    bool flag2 = expressionFace.Mouth;
		    if (flag2)
		    {
			    expressionFace.Mouth.SetColor("_MouthBot", new Color32(205, 190, byte.MaxValue, byte.MaxValue));
			    expressionFace.Mouth.SetColor("_MouthMid", new Color32(182, 170, 226, byte.MaxValue));
			    expressionFace.Mouth.SetColor("_MouthTop", new Color32(182, 170, 205, byte.MaxValue));
		    }
		    bool flag3 = expressionFace.Eyes;
		    if (flag3)
		    {
			    expressionFace.Eyes.SetColor("_EyeRed", new Color32(205, 190, byte.MaxValue, byte.MaxValue));
			    expressionFace.Eyes.SetColor("_EyeGreen", new Color32(182, 170, 226, byte.MaxValue));
			    expressionFace.Eyes.SetColor("_EyeBlue", new Color32(182, 170, 205, byte.MaxValue));
		    }
	    }
	    slimeAppearance.Face.OnEnable();
	    slimeAppearance.Icon = mudSlime.icon;
	    slimeAppearance.ColorPalette = new SlimeAppearance.Palette()
	    {
		    Ammo = mudSlime.color,
		    Top = material.GetColor(TopColor),
		    Middle = material.GetColor(MiddleColor),
		    Bottom = material.GetColor(BottomColor)
	    };
	    mudSlime.AppearancesDefault = new SlimeAppearance[] { slimeAppearance };
	    SlimeAppearanceApplicator slimeAppearanceApplicator = slimeMud.GetComponent<SlimeAppearanceApplicator>();
	    slimeAppearanceApplicator.Appearance = slimeAppearance;
	    slimeAppearanceApplicator.SlimeDefinition = mudSlime;
	    slimeMud.GetComponent<Identifiable>().identType = mudSlime;
	    SlimeEatWater slimeEatWater = slimeMud.GetComponent<SlimeEatWater>();
	    slimeEatWater.puddlePlortIdent = mudPlort;
	    slimeEatWater.plort = mudPlort.prefab;
	    mudSlime.prefab = slimeMud;
	    SlimeRegistry.RegistrySlimeAppearance(identifiableType, slimeAppearance);
	    var dietGroupHighlight = SRObjects.Get<DietGroupHighlight>("DietHighlight");
	    dietGroupHighlight.slimeDietOverrideEntries.Add(new DietGroupHighlight.SlimeDietOverrideEntry()
	    {
		    icon = mudSlime.icon, slime = identTypes[1].Cast<SlimeDefinition>(), label = TranslationPatcher.AddTranslation("UI", "m.foodgroup.mud", "Mud"), 
	    });
	    
	    
	    SlimeRegistry.sceneActions.Add(context =>
	    {
		   
	    } );
    }

    
    public override IdentifiableType[] BuildIdentifiable() => SlimeCreation.CreateSlimeAndPlortsDefinition(Name);
}