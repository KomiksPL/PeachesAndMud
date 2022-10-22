using System.Collections.Generic;
using System.Linq;
using DebuggingMod;
using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher;
using Il2CppMonomiPark.SlimeRancher.DataModel;
using Il2CppMonomiPark.SlimeRancher.UI.MainMenu.Model;
using MelonLoader;
using UnityEngine;
using EntryPoint = SaveFixer.EntryPoint;

[assembly: MelonInfo(typeof(EntryPoint), "SaveFixer", "1.0.0", "KomiksPL")]
[assembly: MelonGame("MonomiPark", "SlimeRancher2")]
namespace SaveFixer;

[HarmonyLib.HarmonyPatch(typeof(SavedGame), nameof(SavedGame.Push), typeof(GameModel))]
public static class PediaModelPull
{
    public static void Prefix(GameModel gameModel, SavedGame __instance)
    {
        List<string> stringsToDelete = new List<string>();
        foreach (string pediaUnlockedId in __instance.gameState.pedia.unlockedIds)
        {
            if (!__instance.pediaEntryLookup.ContainsKey(pediaUnlockedId))
            {
                stringsToDelete.Add(pediaUnlockedId);
            }
        }

        foreach (string VARIABLE in stringsToDelete)
        {
            __instance.gameState.pedia.unlockedIds.Remove(VARIABLE);
        }
    }
}
public class EntryPoint : MelonMod
{
    public override void OnInitializeMelon()
    {
        foreach (var patchedMethod in HarmonyInstance.GetPatchedMethods())
        {
            patchedMethod.Name.LogMessage();
        }
    }
}