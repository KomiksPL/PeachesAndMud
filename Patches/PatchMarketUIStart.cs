using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.UI;
using PAM.Utility;
using UnhollowerBaseLib;
using Enum = Il2CppSystem.Enum;

namespace PAM.Patches;

[HarmonyPatch(typeof(MarketUI), nameof(MarketUI.Start))]
public static class PatchMarketUIStart
{
    
    public static void Prefix(MarketUI __instance)
    {
            
        __instance.plorts = __instance.plorts.Where(x => !PlortRegistry.plortsToPatch.Exists((Predicate<MarketUI.PlortEntry>)(y => y == x))).ToArray();
        __instance.plorts = (Il2CppReferenceArray<MarketUI.PlortEntry>)HarmonyLib.CollectionExtensions.AddRangeToArray<MarketUI.PlortEntry>(__instance.plorts, 
            PlortRegistry.plortsToPatch.ToArray());
    }
}