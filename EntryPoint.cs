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
    public static Type[] AllTypes = execAssembly.GetTypes().Where(type => type.IsSubclassOf(typeof(IdentifiableTypeCreator)) && type.GetCustomAttribute<NoRegistryAttribute>() == null).ToArray();
    public override void OnInitializeMelon()
    {

        
        Action<Scene, LoadSceneMode> loadScene = (scene, mode) =>
        {
            switch (scene)
            {
                case { name: "zoneCore" }:
                {
                    var sceneContext = SRSingleton<SceneContext>.Instance;
                    foreach (var sceneAction in SlimeRegistry.sceneActions)
                    {
                        sceneAction(sceneContext);
                    }

                    break;
                }
                case { name: "GameCore" }:
                {
                    foreach (var identType in IdentifiableTypeCreator.identTypeCreators)
                    {
                        identType.Build();
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

                   

                    break;
                }
            }
        };
        SceneManager.add_sceneLoaded(loadScene);
    }
}