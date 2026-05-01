using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundEntry
{
    public string backgroundName;
    public Sprite backgroundSprite;
}

public class BackgroundLibrary : MonoBehaviour
{
    public List<BackgroundEntry> backgrounds;

    public Sprite GetBackground(string name)
    {
        foreach (var entry in backgrounds)
        {
            if (entry.backgroundName == name)
                return entry.backgroundSprite;
        }
        return null;
    }
}
