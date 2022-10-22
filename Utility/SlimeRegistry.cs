using System;
using System.Collections.Generic;
using DebuggingMod;
using Il2CppMonomiPark.SlimeRancher.Script.Util;
using PAM.Patches;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace PAM.Utility;

public static class SlimeRegistry
{
    public static List<Action<SceneContext>> sceneActions = new List<Action<SceneContext>>();
    public static void RegistrySlimeAppearance(SlimeDefinition identifiableType, SlimeAppearance slimeAppearance)
    {
        slimeAppearance.hideFlags |= HideFlags.HideAndDontSave;
        sceneActions.Add(context =>
        {
            context.SlimeAppearanceDirector.RegisterDependentAppearances(identifiableType, slimeAppearance);
            context.SlimeAppearanceDirector.UpdateChosenSlimeAppearance(identifiableType, slimeAppearance);
        } );
    }

    public static void RegistrySlimeDefinition(SlimeDefinition slimeDefinition)
    {
        slimeDefinition.Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, slimeDefinition);
        
    }
}