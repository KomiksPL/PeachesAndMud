using System;
using System.Collections.Generic;
using DebuggingMod;
using HarmonyLib;
using PAM.Utility;

namespace PAM.Patches;

[HarmonyPatch(typeof(AutoSaveDirector), "Awake")]
public class PatchAutoSaveDirectorAwake
{
    public static List<IdentifiableType> vaccableIdentifiables = new List<IdentifiableType>();

    public static void Prefix(AutoSaveDirector __instance)
    {
        IdentifiableTypeGroup slimesGroup = SRObjects.Get<IdentifiableTypeGroup>("SlimesGroup");
        IdentifiableTypeGroup vaccableNonLiquids = SRObjects.Get<IdentifiableTypeGroup>("VaccableNonLiquids");
        IdentifiableTypeGroup vaccableBaseSlimeGroup = SRObjects.Get<IdentifiableTypeGroup>("VaccableBaseSlimeGroup");
        IdentifiableTypeGroup plortGroup = SRObjects.Get<IdentifiableTypeGroup>("PlortGroup");
        IdentifiableTypeGroup liquidGroup = SRObjects.Get<IdentifiableTypeGroup>("LiquidGroup");
        IdentifiableTypeGroup meatGroup = SRObjects.Get<IdentifiableTypeGroup>("MeatGroup");
        foreach (var allType in EntryPoint.AllTypes)
        {
            var identifiableTypeCreator = Activator.CreateInstance(allType) as IdentifiableTypeCreator;
            var buildIdentifiable = identifiableTypeCreator?.BuildIdentifiable();
            if (identifiableTypeCreator != null)
            {
                identifiableTypeCreator.identTypes = buildIdentifiable;
                IdentifiableTypeCreator.identTypeCreators.Add(identifiableTypeCreator);
            }
        }
        foreach (IdentifiableType createdIdentifiable in LookupRegistry.createdIdentifiables.Values)
        {
            if (createdIdentifiable.TryCast<SlimeDefinition>())
                slimesGroup.memberTypes.Add(createdIdentifiable);
            if (createdIdentifiable.IsPlort)
                plortGroup.memberTypes.Add(createdIdentifiable);
            if (createdIdentifiable.TryCast<LiquidDefinition>())
                liquidGroup.memberTypes.Add(createdIdentifiable);
            
            if (createdIdentifiable.IsAnimal && !createdIdentifiable.name.Contains("Chick"))
                meatGroup.memberTypes.Add(createdIdentifiable);
            if (!createdIdentifiable.name.Contains("Gordo"))
                __instance.identifiableTypes.memberTypes.Add(createdIdentifiable);


        }

        foreach (var identifiableType in vaccableIdentifiables)
        {
            if (identifiableType.TryCast<SlimeDefinition>())
                vaccableBaseSlimeGroup.memberTypes.Add(identifiableType);
            else
            {
                vaccableNonLiquids.memberTypes.Add(identifiableType);
            }
        }
    }
}