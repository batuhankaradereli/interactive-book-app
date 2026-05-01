using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public GameObject notePanel;
    public Button noteButton;
    public TMP_InputField noteInputField;
    public Button saveNoteButton;
    public Button deleteNoteButton;

    private Dictionary<int, string> notes = new Dictionary<int, string>();
    private int currentPage = 0;

    private void Start()
    {
        notePanel.SetActive(false);

        // Butonlara fonksiyonlarý atýyoruz
        noteButton.onClick.AddListener(OpenNotePanel);
        saveNoteButton.onClick.AddListener(SaveNote);
        deleteNoteButton.onClick.AddListener(DeleteNote);

        LoadNotes();
    }

    public void SetCurrentPage(int index)
    {
        currentPage = index;
        LoadNoteForPage();
    }

    public void OpenNotePanel()
    {
        LoadNoteForPage();
        notePanel.SetActive(true);
    }

    public void SaveNote()
    {
        string note = noteInputField.text;

        if (string.IsNullOrWhiteSpace(note))
        {
            if (notes.ContainsKey(currentPage))
                notes.Remove(currentPage); // boþsa notu sil
        }
        else
        {
            notes[currentPage] = note;
        }

        SaveNotesToPrefs();
        notePanel.SetActive(false);
    }

    public void DeleteNote()
    {
        if (notes.ContainsKey(currentPage))
        {
            notes.Remove(currentPage);
            SaveNotesToPrefs();
        }

        noteInputField.text = "";
        notePanel.SetActive(false);
    }

    private void LoadNoteForPage()
    {
        if (notes.TryGetValue(currentPage, out string note))
        {
            noteInputField.text = note;
        }
        else
        {
            noteInputField.text = "";
        }
    }

    private void SaveNotesToPrefs()
    {
        string json = JsonUtility.ToJson(new NoteData(notes));
        PlayerPrefs.SetString("SavedNotes", json);
        PlayerPrefs.Save();
    }

    private void LoadNotes()
    {
        if (PlayerPrefs.HasKey("SavedNotes"))
        {
            string json = PlayerPrefs.GetString("SavedNotes");
            NoteData data = JsonUtility.FromJson<NoteData>(json);
            notes = data.ToDictionary();
        }
    }

    public bool HasNoteForPage()
    {
        return notes.ContainsKey(currentPage);
    }

    [System.Serializable]
    public class NoteData
    {
        public List<int> pageIndices = new List<int>();
        public List<string> noteTexts = new List<string>();

        public NoteData(Dictionary<int, string> dict)
        {
            foreach (var pair in dict)
            {
                pageIndices.Add(pair.Key);
                noteTexts.Add(pair.Value);
            }
        }

        public Dictionary<int, string> ToDictionary()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < pageIndices.Count; i++)
            {
                dict[pageIndices[i]] = noteTexts[i];
            }
            return dict;
        }
    }
}
