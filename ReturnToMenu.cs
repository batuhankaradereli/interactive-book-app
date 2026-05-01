using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void GoToMenu()
    {
        Time.timeScale = 1f; // Eðer sahne durdurulmuþsa normale dönsün
        SceneManager.LoadScene("BookSelectionScene");
    }
}
