using System;
using Il2CppMonomiPark.SlimeRancher.Script.Util;
using PAM.Patches;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace PAM.Utility;

public static class TranslationPatcher
{
    public static LocalizedString AddTranslation(string table, string key, string localized)
    {
        PatchLocalizationDirectorLoadTable.translations.Add(new Tuple<string, string, string>(table, key, localized));
        StringTable sharedDataTableCollectionName = LocalizationUtil.GetTable(table);
        StringTableEntry stringTableEntry = sharedDataTableCollectionName.AddEntry(key, localized);
        return new LocalizedString(sharedDataTableCollectionName.SharedData.TableCollectionNameGuid, stringTableEntry.SharedEntry.Id);
    }
}