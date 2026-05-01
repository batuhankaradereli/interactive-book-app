using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public List<Button> optionButtons;
    public GameObject nextButton;
    public GameObject previousButton;
    public GameObject returnToMenuButton;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI pageNumberText;
    public Animator pageAnimator;
    public GameObject bookPanel;

    private List<QuizQuestion> questions;
    private int currentQuestionIndex = 0;
    private int score = 0;
    private List<int> selectedAnswers = new List<int>();

    void Start()
    {
        string selectedBook = GameManager.GetSelectedBook();
        string filePath = Path.Combine(Application.streamingAssetsPath, selectedBook);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            QuizQuestion[] questionArray = JsonHelper.FromJsonArray<QuizQuestion>(json);
            questions = new List<QuizQuestion>(questionArray);
        }
        else
        {
            Debug.LogError("Quiz JSON dosyası bulunamadı: " + filePath);
            return;
        }

        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("Sorular yüklenemedi veya boş.");
            return;
        }

        selectedAnswers = new List<int>(new int[questions.Count]);

        if (returnToMenuButton != null)
            returnToMenuButton.SetActive(false);

        DisplayQuestion();
    }


    public void DisplayQuestion()
    {
        if (currentQuestionIndex < 0 || currentQuestionIndex >= questions.Count)
        {
            Debug.LogWarning("Geçersiz soru indeksi.");
            return;
        }

        QuizQuestion currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.question;

        for (int i = 0; i < optionButtons.Count; i++)
        {
            var text = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = currentQuestion.options[i];

            optionButtons[i].interactable = true;
            ColorBlock cb = optionButtons[i].colors;
            cb.normalColor = Color.white;
            optionButtons[i].colors = cb;
        }

        pageNumberText.text = (currentQuestionIndex + 1) + " / " + questions.Count;

        if (returnToMenuButton != null)
            returnToMenuButton.SetActive(false); // Her soruda yeniden gizle
    }

    public void OnOptionSelected(int index)
    {
        Debug.Log("Şık tıklandı: " + index);

        selectedAnswers[currentQuestionIndex] = index;

        foreach (Button button in optionButtons)
            button.interactable = false;

        QuizQuestion currentQuestion = questions[currentQuestionIndex];

        if (index == currentQuestion.correctIndex)
        {
            score++;
            Debug.Log("Doğru cevap!");
        }

        var colors = optionButtons[index].colors;
        colors.normalColor = Color.yellow;
        optionButtons[index].colors = colors;
    }

    public void OnNextButtonClicked()
    {
        Debug.Log("Next button clicked");

        if (currentQuestionIndex < questions.Count - 1)
        {
            currentQuestionIndex++;
            pageAnimator.SetTrigger("TurnNext");
            DisplayQuestion();
        }
        else
        {
            ShowAnswerKey();
        }
    }

    public void OnPreviousButtonClicked()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            pageAnimator.SetTrigger("TurnPrev");
            DisplayQuestion();
        }
    }

    public void ShowAnswerKey()
    {
        questionText.text = "";
        foreach (Button button in optionButtons)
            button.gameObject.SetActive(false);

        string selectedBook = GameManager.GetSelectedBook();
        string badge = "";

        if (selectedBook.Contains("Sokrates"))
        {
            if (score <= 3)
                badge = "Sorgulayan Zihin";
            else if (score <= 7)
                badge = "Bilgelik Yolcusu";
            else
                badge = "Gerçek Filozof";
        }
        else
        {
            badge = "Başarılı Okuyucu";
        }

        if (selectedBook.Contains("ThreeMusketeers"))
        {
            if (score <= 3)
                badge = "Acemi Silahşör";
            else if (score <= 7)
                badge = "Onurlu Muhafız";
            else
                badge = "Gerçek Silahşör";
        }
        else
        {
            badge = "Başarılı Okuyucu";
        }

        string resultSummary = "Doğru Cevap Sayısı: " + score + "/" + questions.Count + "\n";
        resultSummary += "Kazanılan Rozet: " + badge + "\n\n";
        resultSummary += "<b>Doğru Cevaplar:</b>\n";

        for (int i = 0; i < questions.Count; i++)
        {
            string correctAnswer = questions[i].options[questions[i].correctIndex];
            resultSummary += (i + 1) + ". " + correctAnswer + "\n";
        }

        resultText.text = resultSummary;

        if (returnToMenuButton != null)
            returnToMenuButton.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("BookSelectionScene"); // ← düzeltilmiş sahne adı
    }
}

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] options;
    public int correctIndex;
}
