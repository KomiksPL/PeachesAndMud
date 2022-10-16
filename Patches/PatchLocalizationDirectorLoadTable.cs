using System;
using System.Collections;
using System.Collections.Generic;
using DebuggingMod;
using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.UI.Localization;
using MelonLoader;
using UnityEngine;

namespace PAM.Patches
{
    [HarmonyPatch(typeof(LocalizationDirector), "LoadTables")]
    public static class PatchLocalizationDirectorLoadTable
    {
        private static IEnumerator LoadTable(LocalizationDirector director)
        {
            WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0.01f);
            yield return waitForSecondsRealtime;
            foreach (Tuple<string, string, string> translation in translations)
            {
                director.Tables[translation.Item1].AddEntry(translation.Item2, translation.Item3);
            }
        }

        public static void Postfix(LocalizationDirector __instance)
        {
            MelonCoroutines.Start(LoadTable(__instance));
        }
        public static List<Tuple<string, string, string>> translations = new List<Tuple<string, string, string>>();
    }
}