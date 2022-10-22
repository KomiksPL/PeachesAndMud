using System.Collections.Generic;

namespace PAM.Utility;

public class IdentifiableTypeCreator
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual IdentifiableType[] identTypes { get; set; } = null;
    internal static List<IdentifiableTypeCreator> identTypeCreators = new List<IdentifiableTypeCreator>();


    public virtual void Build()
    {

    }

    public virtual IdentifiableType[] BuildIdentifiable()
    {
        return null;
    }
}