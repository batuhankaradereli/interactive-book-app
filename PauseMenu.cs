using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button muteButton;
    public Slider volumeSlider;

    private bool isPaused = false;
    private bool isMuted = false;

    void Start()
    {
        if (volumeSlider != null && AudioManager.Instance != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            volumeSlider.value = savedVolume;
            AudioManager.Instance.SetVolume(savedVolume);

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ResumeMusic();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PauseMusic();
        }
    }

    public void SetVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(volume);
        }
    }

    public void ToggleMute()
    {
        if (AudioManager.Instance == null) return;

        if (!isMuted)
        {
            AudioManager.Instance.UpdatePreviousVolume();
            AudioManager.Instance.Mute();
            isMuted = true;
        }
        else
        {
            AudioManager.Instance.Unmute();
            isMuted = false;
        }
    }


    public void SaveGame()
    {
        DialogueManager dm = FindAnyObjectByType<DialogueManager>();
        if (dm != null)
            dm.SaveProgress();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BookSelectionScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
