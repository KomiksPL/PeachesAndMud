using System;
using System.Linq;
using DebuggingMod;
using HarmonyLib;
using PAM.Utility;

namespace PAM.Patches;

[HarmonyPatch(typeof(PediaDirector), nameof(PediaDirector.Awake))]
public static class PatchPediaDirectorAwake
{
    public static void Prefix(PediaDirector __instance)
    {
        //TODO End this
        /*var pediaEntryCategory = __instance.entryCategories.items.ToArray().First(x => x.name == "Slimes");
        var identifiablePediaEntry = ScriptableObject.CreateInstance<IdentifiablePediaEntry>();
        identifiablePediaEntry.hideFlags |= HideFlags.HideAndDontSave;
        identifiablePediaEntry.name = "Mud";
        identifiablePediaEntry.template = SRObjects.Get<PediaTemplate>("SlimePediaTemplate");
        var mudSlime = LookupRegistry.GetIdentifiableByName("Mud");
        identifiablePediaEntry.identifiableType = mudSlime;
     
        identifiablePediaEntry.title = TranslationPatcher.AddTranslation("Pedia", "t.mud_slime", "Mud Slime");
        identifiablePediaEntry.description = TranslationPatcher.AddTranslation("Pedia", "m.intro.peach_slime", "Intro in mud slime");
        TranslationPatcher.AddTranslation("Pedia", "m.desc.mud_slme.page.1", "Mud Slime");
        
        
        Func<bool> isAvailable = () => true;
        Func<bool> isHidden = () => false;
        identifiablePediaEntry.IsAvailable = isAvailable;
        identifiablePediaEntry.isHidden = isHidden;
        identifiablePediaEntry.isUnlockedInitially = true;
        pediaEntryCategory.items.Add(identifiablePediaEntry);
        __instance.identDict.Add(mudSlime, identifiablePediaEntry);
        */
        
        
    }
}