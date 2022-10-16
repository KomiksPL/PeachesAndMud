using System.Collections.Generic;
using System.Linq;
using PAM.Patches;

namespace PAM.Utility;

public static class LookupRegistry
{
    internal static Dictionary<string, IdentifiableType> createdIdentifiables = new Dictionary<string, IdentifiableType>();
    public static IdentifiableType GetIdentifiableByName(string name)
    {
        return createdIdentifiables.ContainsKey(name) ? createdIdentifiables[name] : null;
    }

    public static void RegistryVaccable(IdentifiableType identifiableType)
    {
        PatchAutoSaveDirectorAwake.vaccableIdentifiables.Add(identifiableType);
    }

    public static IdentifiableType GetVanillaIdentifiableByName(string name) =>
        Resources.FindObjectsOfTypeAll<IdentifiableType>().FirstOrDefault(x => x.name == name);

    public static IdentifiableTypeGroup GetIdentifiableTypeGroup(string name) =>
        Resources.FindObjectsOfTypeAll<IdentifiableTypeGroup>().FirstOrDefault(x => x.name == name);
    public static void RegistryIdentifiable(IdentifiableType type)
    {

        type.hideFlags |= HideFlags.HideAndDontSave;
        createdIdentifiables.Add(type.name, type);
    }
    /*public static void RegisterVaccable(IdentifiableType slimeDefinition)
    {
        PatchAutoSaveDirectorAwake.vaccableIdentifiables.Add(slimeDefinition);
    }
    */
}