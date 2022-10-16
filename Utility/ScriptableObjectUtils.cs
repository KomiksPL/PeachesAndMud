using System;

namespace PAM.Utility;

public class ScriptableObjectUtils
{
    public static T CreateScriptable<T>(Action<T> constructor = null) where T : ScriptableObject
    {
        T instance = ScriptableObject.CreateInstance<T>();
        constructor?.Invoke(instance);
        return instance;
    }
}