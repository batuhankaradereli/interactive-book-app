using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookSelectionManager : MonoBehaviour
{
    public void OnBookSelected(string jsonFileName)
    {
        GameManager.SetSelectedBook(jsonFileName);
        GameManager.SetSavedIndex(0);

        if (jsonFileName == "Sokrates.json")
        {
            SceneManager.LoadScene("SokratesScene");
        }
        else if (jsonFileName == "ThreeMusketeers.json")
        {
            SceneManager.LoadScene("ThreeMusketeersScene");
        }
        else
        {
            Debug.LogError("Tanımsız kitap: " + jsonFileName);
        }
    }

    public void OnContinueSokrates()
    {
        string savedBook = GameManager.GetSelectedBook();

        if (savedBook == "Sokrates.json")
        {
            SceneManager.LoadScene("SokratesScene");
        }
        else
        {
            Debug.LogWarning("Sokrates için kayıtlı kitap bulunamadı veya eşleşmedi.");
        }
    }

    public void OnContinueThreeMusketeers()
    {
        string savedBook = GameManager.GetSelectedBook();

        if (savedBook == "ThreeMusketeers.json")
        {
            SceneManager.LoadScene("ThreeMusketeersScene");
        }
        else
        {
            Debug.LogWarning("Three Musketeers için kayıtlı kitap bulunamadı veya eşleşmedi.");
        }
    }
    public void QuitGame()
    {
        Debug.Log("Oyun kapatılıyor...");
        Application.Quit();
    }

    public void OpenSokratesQuiz()
    {
        GameManager.SetSelectedBook("SokratesQuiz.json");
        SceneManager.LoadScene("SokratesQuizScene");
    }

    public void OpenThreeMusketeersQuiz()
    {
        GameManager.SetSelectedBook("ThreeMusketeersQuiz.json");
        SceneManager.LoadScene("ThreeMusketeersQuizScene");
    }

}
