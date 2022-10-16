namespace PAM.Utility;


public static class PrefabUtils
{
    public static Transform DisabledParent;
    static PrefabUtils()
    {
        DisabledParent = new GameObject("DeactivedObject").transform;
        DisabledParent.gameObject.SetActive(false);
        Object.DontDestroyOnLoad(DisabledParent.gameObject);
        DisabledParent.gameObject.hideFlags |= HideFlags.HideAndDontSave;
    }

    public static GameObject CopyPrefab(GameObject prefab)
    {
        var newG = Object.Instantiate(prefab, DisabledParent);
        return newG;
    }
}