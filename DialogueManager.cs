using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public string jsonFileName = "";
    public TextMeshProUGUI dialogueText;
    public GameObject namePanel;
    public TextMeshProUGUI characterNameText;
    public UnityEngine.UI.Image characterPortrait;
    public PortraitLibrary portraitLibrary;
    public TextMeshProUGUI pageNumberText;
    public Animator pageAnimator;
    public GameObject returnToMenuButton;


    public Image backgroundImage;
    public BackgroundLibrary backgroundLibrary;

    private List<DialogueLine> dialogues;
    private int currentIndex = 0;

    private NoteManager noteManager; // 🔹 Yeni: Not sistemi bağlantısı

    void Start()
    {
        jsonFileName = GameManager.GetSelectedBook();

        if (string.IsNullOrEmpty(jsonFileName))
        {
            Debug.LogError("jsonFileName boş geldi. BookSelection sahnesinden doğru şekilde aktarılmadı.");
            return;
        }

        bool loaded = LoadDialogue(); // JSON dosyası düzgün yüklendi mi?

        if (loaded)
        {
            currentIndex = GameManager.GetSavedIndex();
            DisplayCurrentDialogue();
            PlayPageTurnAnimation();

            // 🔹 Yeni: Not sistemini bağla
            noteManager = FindFirstObjectByType<NoteManager>();
            if (noteManager != null)
                noteManager.SetCurrentPage(currentIndex);
        }
        else
        {
            Debug.LogError("Diyalog verileri yüklenemedi.");
        }
    }

    bool LoadDialogue()
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            try
            {
                DialogueData data = JsonUtility.FromJson<DialogueData>(json);
                if (data != null && data.Items != null && data.Items.Length > 0)
                {
                    dialogues = new List<DialogueLine>(data.Items);
                    return true;
                }
            }
            catch { }

            try
            {
                DialogueDataAlt dataAlt = JsonUtility.FromJson<DialogueDataAlt>(json);
                if (dataAlt != null && dataAlt.lines != null && dataAlt.lines.Length > 0)
                {
                    dialogues = new List<DialogueLine>();
                    foreach (var line in dataAlt.lines)
                    {
                        dialogues.Add(new DialogueLine
                        {
                            character = line.speaker,
                            text = line.text,
                            portrait = $"{line.speaker}_{line.emotion}",
                            background = line.background
                        });
                    }
                    return true;
                }
            }
            catch { }

            Debug.LogError("JSON içeriği boş veya hatalı.");
        }
        else
        {
            Debug.LogError("JSON dosyası bulunamadı: " + path);
        }

        return false;
    }

    public void DisplayCurrentDialogue()
    {
        if (dialogues == null || currentIndex >= dialogues.Count)
        {
            dialogueText.text = "Tüm diyaloglar bitti.";
            return;
        }

        DialogueLine line = dialogues[currentIndex];
        dialogueText.text = line.text;
        characterNameText.text = line.character;

        // Portre
        if (portraitLibrary != null && !string.IsNullOrEmpty(line.portrait))
        {
            Sprite newPortrait = portraitLibrary.GetPortrait(line.portrait);
            if (newPortrait != null)
                characterPortrait.sprite = newPortrait;
        }

        // Arka Plan
        if (backgroundLibrary != null && !string.IsNullOrEmpty(line.background))
        {
            Sprite newBackground = backgroundLibrary.GetBackground(line.background);
            if (newBackground != null && backgroundImage != null)
                backgroundImage.sprite = newBackground;
        }
        if (returnToMenuButton != null)
        {
            returnToMenuButton.SetActive(currentIndex == dialogues.Count - 1);
        }


        namePanel.SetActive(!string.IsNullOrEmpty(line.character));
        pageNumberText.text = $"Sayfa {currentIndex + 1} / {dialogues.Count}";

        // 🔹 Yeni: Not sistemine mevcut sayfayı bildir
        if (noteManager != null)
            noteManager.SetCurrentPage(currentIndex);
    }

    public void NextLine()
    {
        if (currentIndex < dialogues.Count - 1)
        {
            currentIndex++;
            PlayPageTurnAnimation();
            GameManager.SetSavedIndex(currentIndex);
            DisplayCurrentDialogue();

            // 🔹 Yeni: Not güncelle
            if (noteManager != null)
                noteManager.SetCurrentPage(currentIndex);
        }
    }

    public void PreviousLine()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            PlayPageTurnAnimation();
            GameManager.SetSavedIndex(currentIndex);
            DisplayCurrentDialogue();

            // 🔹 Yeni: Not güncelle
            if (noteManager != null)
                noteManager.SetCurrentPage(currentIndex);
        }
    }

    public void PlayPageTurnAnimation()
    {
        if (pageAnimator != null)
        {
            pageAnimator.SetTrigger("PlayTurn");
        }
    }

    public void SaveProgress()
    {
        GameManager.SetSavedIndex(currentIndex);
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
