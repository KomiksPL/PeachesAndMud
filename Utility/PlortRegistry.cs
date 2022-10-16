using System.Collections.Generic;
using Il2CppMonomiPark.SlimeRancher.UI;

namespace PAM.Utility;

public class PlortRegistry
{
    public static List<MarketUI.PlortEntry> plortsToPatch = new List<MarketUI.PlortEntry>();
    public static List<EconomyDirector.ValueMap> valueMapsToPatch = new List<EconomyDirector.ValueMap>();
    public static void RegisterPlort(IdentifiableType id, float value, float fullSaturationValue)
    {
        plortsToPatch.Add(new MarketUI.PlortEntry()
        {
            identType = id
        });
        valueMapsToPatch.Add(new EconomyDirector.ValueMap()
        {
            accept = id.prefab.GetComponent<Identifiable>(),
            fullSaturation = fullSaturationValue,
            value = value
        });
    }


}