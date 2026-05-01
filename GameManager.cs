using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string selectedBook;
    private int savedIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public static void SetSelectedBook(string bookName)
    {
        if (Instance != null)
            Instance.selectedBook = bookName;
    }

    public static string GetSelectedBook()
    {
        return Instance != null ? Instance.selectedBook : null;
    }

    public static void SetSavedIndex(int index)
    {
        if (Instance != null)
            Instance.savedIndex = index;
    }

    public static int GetSavedIndex()
    {
        return Instance != null ? Instance.savedIndex : 0;
    }

    public static bool HasSavedGame()
    {
        return Instance != null && !string.IsNullOrEmpty(Instance.selectedBook);
    }

    
    public static string GetSavedBook()
    {
        return GetSelectedBook();
    }
}
