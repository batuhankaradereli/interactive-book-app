using UnityEngine;

public static class SaveManager
{
    private const string SaveKey = "SavedDialogueIndex";

    public static void SaveProgress(int currentIndex)
    {
        PlayerPrefs.SetInt(SaveKey, currentIndex);
        PlayerPrefs.Save();
        Debug.Log("Oyun kaydedildi. Index: " + currentIndex);
    }

    public static int LoadProgress()
    {
        return PlayerPrefs.GetInt(SaveKey, 0);
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey(SaveKey);
    }
}
