using System.Linq;
using HarmonyLib;
using Il2CppSystem;
using PAM.Utility;

namespace PAM.Patches;

[HarmonyLib.HarmonyPatch(typeof(EconomyDirector), nameof(EconomyDirector.InitModel))]
public static class PatchEconomyDirectorInitModel
{
    public static void Prefix(EconomyDirector __instance) => __instance.baseValueMap = __instance.baseValueMap.ToArray().AddRangeToArray<EconomyDirector.ValueMap>(PlortRegistry.valueMapsToPatch.ToArray());

}