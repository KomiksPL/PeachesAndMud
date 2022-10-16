using System;
using System.Collections.Generic;
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
        foreach (var allType in EntryPoint.AllTypes)
        {
            if (allType.Namespace != null&& !allType.Namespace.Contains("Identifiables")) continue;
            allType.GetMethod("BuildForAutoSave")?.Invoke(null, Array.Empty<object>());
        }
        foreach (IdentifiableType createdIdentifiable in LookupRegistry.createdIdentifiables.Values)
        {
            if (createdIdentifiable.TryCast<SlimeDefinition>())
                slimesGroup.memberTypes.Add(createdIdentifiable);
            if (createdIdentifiable.IsPlort)
                plortGroup.memberTypes.Add(createdIdentifiable);
            if (createdIdentifiable.TryCast<LiquidDefinition>())
                liquidGroup.memberTypes.Add(createdIdentifiable);
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