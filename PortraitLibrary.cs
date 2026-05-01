using System.Collections.Generic;
using UnityEngine;

public class PortraitLibrary : MonoBehaviour
{
    [System.Serializable]
    public class PortraitEntry
    {
        public string name;
        public Sprite sprite;
    }

    public List<PortraitEntry> portraits;

    public Sprite GetPortrait(string portraitName)
    {
        foreach (var entry in portraits)
        {
            if (entry.name == portraitName)
            {
                return entry.sprite;
            }
        }

        Debug.LogWarning("Portre bulunamadý: " + portraitName);
        return null;
    }
}
