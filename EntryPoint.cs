using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DebuggingMod;
using HarmonyLib;
using MelonLoader;
using PAM.Utility;
using Sony.NP;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine.SceneManagement;

namespace PAM;

public class EntryPoint : MelonMod
{
        
    public static Assembly execAssembly = Assembly.GetExecutingAssembly();
    public static Type[] AllTypes = execAssembly.GetTypes();
    public override void OnInitializeMelon()
    {

        Action<Scene, LoadSceneMode> loadScene = (scene, mode) =>
        {
            if (scene.name.Equals("zoneCore"))
            {
                var sceneContext = SRSingleton<SceneContext>.Instance;
                foreach (var sceneAction in SlimeRegistry.sceneActions)
                {
                    sceneAction(sceneContext);
                }
            }
            if (scene.name.Equals("GameCore"))
            {
                foreach (var type in AllTypes)
                {
                    if (type.Namespace != null&& !type.Namespace.Contains("Identifiables")) continue;
                    type.GetMethod("Build")?.Invoke(null, Array.Empty<object>());
                }

                foreach (var identifiables in LookupRegistry.createdIdentifiables.Values)
                {
                    var slimeDefinition = identifiables.TryCast<SlimeDefinition>();
                    if (slimeDefinition)
                    {
                        var slimeDefinitions = SRSingleton<GameContext>.Instance.SlimeDefinitions;
                    
                        slimeDefinitions.Slimes = slimeDefinitions.Slimes.AddItem(slimeDefinition).ToArray();
                        slimeDefinitions.slimeDefinitionsByIdentifiable.Add(slimeDefinition, slimeDefinition);
                    }

                }
            }

          

        };
        SceneManager.add_sceneLoaded(loadScene);
    }
}