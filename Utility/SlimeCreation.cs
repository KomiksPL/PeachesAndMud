namespace PAM.Utility;

public static class SlimeCreation
{
    public static IdentifiableType[] CreateSlimeAndPlortsDefinition(string name)
    {
        SlimeDefinition slime = ScriptableObjectUtils.CreateScriptable<SlimeDefinition>(delegate(SlimeDefinition definition)
        {
            definition.name = name;
            definition.referenceId = "IdentifiableType." + definition.name;
            definition.localizedName = TranslationPatcher.AddTranslation("Actor", $"l.{name.ToLowerInvariant()}_slime", name +" Slime");
            definition.icon = pam.LoadAsset<Sprite>("iconSlime" + name);
            definition.color = ColorUtils.AverageColorFromTexture(definition.icon);
        });
        IdentifiableType plort = ScriptableObjectUtils.CreateScriptable<IdentifiableType>(delegate(IdentifiableType definition)
        {
            definition.name = name + "Plort";
            definition.color = ColorUtils.FromHex("ff00ae");
            definition.referenceId = "IdentifiableType." + definition.name;
            definition.localizedName = TranslationPatcher.AddTranslation("Actor", $"l.{name.ToLowerInvariant()}_plort", name + " Plort");
            definition.icon = pam.LoadAsset<Sprite>("iconPlort" + name);
            definition.color = ColorUtils.AverageColorFromTexture(definition.icon);
            definition.IsPlort = true;
        });

        LookupRegistry.RegistryIdentifiable(slime);
        LookupRegistry.RegistryVaccable(slime);
        LookupRegistry.RegistryIdentifiable(plort);
        //LookupRegistry.RegisterVaccable(plort);

        return new IdentifiableType[] {plort, slime };
    }
}