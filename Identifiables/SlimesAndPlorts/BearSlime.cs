using DebuggingMod;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using PAM.Utility;
using UnhollowerBaseLib;
using Object = UnityEngine.Object;

namespace PAM.Identifiables.SlimesAndPlorts;

public static class Bear
{
    public static void Build()
    {
	    string name = "Bear";
        IdentifiableType plort = LookupRegistry.GetIdentifiableByName(name +"Plort");
        GameObject plortPrefab = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("plortPink"));
        plortPrefab.name = "plort" + name;
        plortPrefab.GetComponent<Identifiable>().identType = plort;
        plortPrefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
        var meshRenderer = plortPrefab.GetComponent<MeshRenderer>();
        plort.prefab = plortPrefab;
        
        PlortRegistry.RegisterPlort(plort, 120, 1);
        SlimeDefinition slimeDefinition = LookupRegistry.GetIdentifiableByName(name).Cast<SlimeDefinition>();
	    GameObject slime = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("slimePink"));
	    slime.name = "slime" + name;
	    
	    SlimeDefinition identifiableType = slime.GetComponent<Identifiable>().identType.Cast<SlimeDefinition>();
	    //slimeDefinition.Diet = identifiableType.Diet;

	    slimeDefinition.Diet = new SlimeDiet
	    {
		    ProduceIdents = new[]
		    {
			    plort
		    },
		    FavoriteIdents = new []
		    {
			    LookupRegistry.GetIdentifiableByName("MuddyHen")
		    },
		    BecomesOnTarrifyIdentifiableType = identifiableType.Diet.BecomesOnTarrifyIdentifiableType,
		    MajorFoodGroups = new[]
		    {
			    SlimeEat.FoodGroup.FRUIT
		    },
		    EdiblePlortIdentifiableTypeGroup = identifiableType.Diet.EdiblePlortIdentifiableTypeGroup, 
		    FavoriteProductionCount = 2, 
		    AdditionalFoodIdents = System.Array.Empty<IdentifiableType>(),
		    EatMap = new List<SlimeDiet.EatMapEntry>(),
		    MajorFoodIdentifiableTypeGroups = new[]
		    {
			    LookupRegistry.GetIdentifiableTypeGroup("MeatGroup")
		    }
	    };
	    SlimeRegistry.RegistrySlimeDefinition(slimeDefinition);
	    LookupRegistry.RegistryIdentifiable(slimeDefinition);

	    
	    slimeDefinition.nativeZones = identifiableType.nativeZones;
	    slimeDefinition.Sounds = identifiableType.Sounds;
	    slimeDefinition.properties = identifiableType.properties;


	    var definition = LookupRegistry.GetVanillaIdentifiableByName("Tabby").Cast<SlimeDefinition>();
	    SlimeAppearance slimeAppearance = Object.Instantiate<SlimeAppearance>(definition.AppearancesDefault[0]);
	    slimeAppearance.name = name + "Default";
	    slimeAppearance.Icon = slimeDefinition.icon;	
	    
	    var material = Object.Instantiate(definition.AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
	    material.hideFlags |= HideFlags.HideAndDontSave;
	    material.SetColor("_TopColor", ColorUtils.FromHex("805E5C"));
	    material.SetColor("_BottomColor", ColorUtils.FromHex("654321"));
	    
	    
	    meshRenderer.sharedMaterial = material;
	    slimeAppearance.Structures[0].DefaultMaterials[0] = material;
	    foreach (SlimeExpressionFace expressionFace in slimeAppearance.Face.ExpressionFaces)
	    {
		    if (expressionFace.Mouth)
		    {
			    expressionFace.Mouth.SetColor("_MouthBot", new Color32(205, 190, byte.MaxValue, byte.MaxValue));
			    expressionFace.Mouth.SetColor("_MouthMid", new Color32(182, 170, 226, byte.MaxValue));
			    expressionFace.Mouth.SetColor("_MouthTop", new Color32(182, 170, 205, byte.MaxValue));
		    }

		    if (expressionFace.Eyes)
		    {
			    expressionFace.Eyes.SetColor("_EyeRed", new Color32(205, 190, byte.MaxValue, byte.MaxValue));
			    expressionFace.Eyes.SetColor("_EyeGreen", new Color32(182, 170, 226, byte.MaxValue));
			    expressionFace.Eyes.SetColor("_EyeBlue", new Color32(182, 170, 205, byte.MaxValue));
		    }
	    }

	    slimeAppearance.Face.OnEnable();
	    slimeAppearance.SetSplatColor();
	    
	    slimeAppearance.ColorPalette = new SlimeAppearance.Palette()
	    {
		    Ammo = slimeDefinition.color,
		    Top = material.GetColor(TopColor),
		    Middle = material.GetColor(MiddleColor),
		    Bottom = material.GetColor(BottomColor)
	    };
	    slimeDefinition.AppearancesDefault = new SlimeAppearance[] { slimeAppearance };
	    
	    SlimeAppearanceApplicator slimeAppearanceApplicator = slime.GetComponent<SlimeAppearanceApplicator>();
	    slimeAppearanceApplicator.Appearance = slimeAppearance;
	    slimeAppearanceApplicator.SlimeDefinition = slimeDefinition;
	    slime.GetComponent<Identifiable>().identType = slimeDefinition;
	    SlimeEat slimeEat = slime.GetComponent<SlimeEat>();
	    slimeEat.slimeDefinition = slimeDefinition;
	    slimeDefinition.prefab = slime;
	    SlimeRegistry.RegistrySlimeAppearance(identifiableType, slimeAppearance);
	    
    }

    public static void BuildForAutoSave() => SlimeCreation.CreateSlimeAndPlortsDefinition("Bear");
}