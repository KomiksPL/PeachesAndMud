using DebuggingMod;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using PAM.Utility;
using UnhollowerBaseLib;
using Object = UnityEngine.Object;

namespace PAM.Identifiables.SlimesAndPlorts;

public class GummySlime : IdentifiableTypeCreator
{

	public override string Name => "Gummy";
	public override void Build()
    {
        IdentifiableType plort = identTypes[0];
        GameObject plortPrefab = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("plortPink"));
        plortPrefab.name = "plort" + Name;
        plortPrefab.GetComponent<Identifiable>().identType = plort;
        plortPrefab.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
        var meshRenderer = plortPrefab.GetComponent<MeshRenderer>();
        plort.prefab = plortPrefab;
        
        
        PlortRegistry.RegisterPlort(plort, 120, 1);
        SlimeDefinition slimeDefinition = identTypes[1].Cast<SlimeDefinition>();
	    GameObject slime = PrefabUtils.CopyPrefab(SRObjects.Get<GameObject>("slimePink"));
	    slime.name = "slime" + Name;
	    
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
			    LookupRegistry.GetVanillaIdentifiableByName("BeetVeggie")
		    },
		    BecomesOnTarrifyIdentifiableType = LookupRegistry.GetVanillaIdentifiableByName("Tarr"),
		    MajorFoodGroups = new[]
		    {
			    SlimeEat.FoodGroup.VEGGIES
		    },
		    EdiblePlortIdentifiableTypeGroup = identifiableType.Diet.EdiblePlortIdentifiableTypeGroup, 
		    FavoriteProductionCount = 2, 
		    AdditionalFoodIdents = System.Array.Empty<IdentifiableType>(),
		    EatMap = new List<SlimeDiet.EatMapEntry>(),
		    MajorFoodIdentifiableTypeGroups = new[]
		    {
			    LookupRegistry.GetIdentifiableTypeGroup("VeggieGroup")
		    }
	    };
	    SlimeRegistry.RegistrySlimeDefinition(identifiableType);

	    //Object.Instantiate(slimeDefinition.Diet);
	    
	    slimeDefinition.nativeZones = identifiableType.nativeZones;
	    slimeDefinition.Sounds = identifiableType.Sounds;
	    slimeDefinition.properties = identifiableType.properties;





	    SlimeAppearance slimeAppearance = Object.Instantiate<SlimeAppearance>(identifiableType.AppearancesDefault[0]);
	    slimeAppearance.name = Name + "Default";
	    slimeAppearance.Icon = slimeDefinition.icon;
	    
	    var material = Object.Instantiate(identifiableType.AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
	    material.hideFlags |= HideFlags.HideAndDontSave;
	    material.SetColor("_TopColor", ColorUtils.FromHex("de21b5"));
	    material.SetColor("_MiddleColor", ColorUtils.FromHex("8b66e1"));
	    material.SetColor("_BottomColor", ColorUtils.FromHex("0543b0"));

	    slimeAppearance.Structures[0].DefaultMaterials[0] = material;
	    meshRenderer.sharedMaterial = material;


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

	public override IdentifiableType[] BuildIdentifiable() => SlimeCreation.CreateSlimeAndPlortsDefinition(Name);

}