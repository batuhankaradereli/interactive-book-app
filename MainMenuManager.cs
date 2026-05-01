using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartBook()
    {
        SceneManager.LoadScene("SokratesScene");
    }
}
