using DebuggingMod;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;

namespace PAM.Patches;

[HarmonyPatch(typeof(AnalyticsUtil), nameof(AnalyticsUtil.ReportPerIdentifiableData))]
public static class PatchAnalyticsUtilReportPerIdentifiableData
{
    public static bool Prefix(IEnumerable<IdentifiableType> ids)
    {


        return false;
    }
}