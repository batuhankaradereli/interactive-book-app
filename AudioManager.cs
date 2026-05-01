using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Setup")]
    public AudioSource musicSource;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip[] sokratesClips;
    [SerializeField] private AudioClip[] musketeerClips;

    private int currentIndex = 0;
    private string currentScene = "";
    private bool isPaused = false;
    private float previousVolume;

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
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        currentIndex = 0;
        isPaused = false;
        PlayMusicForScene(currentScene);
    }

    private void Update()
    {
        if (!isPaused && !musicSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "BookSelectionScene")
        {
            musicSource.clip = mainMenuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        else if (sceneName == "SokratesScene")
        {
            PlayNextClipFromList(sokratesClips);
        }
        else if (sceneName == "ThreeMusketeersScene")
        {
            PlayNextClipFromList(musketeerClips);
        }
        else
        {
            musicSource.Stop();
        }
    }

    void PlayNextClip()
    {
        if (currentScene == "SokratesScene" && sokratesClips.Length > 0)
        {
            PlayNextClipFromList(sokratesClips);
        }
        else if (currentScene == "ThreeMusketeersScene" && musketeerClips.Length > 0)
        {
            PlayNextClipFromList(musketeerClips);
        }
    }

    void PlayNextClipFromList(AudioClip[] clips)
    {
        if (clips.Length == 0) return;

        musicSource.clip = clips[currentIndex];
        musicSource.loop = false;
        musicSource.Play();

        currentIndex = (currentIndex + 1) % clips.Length;
    }

    public void PauseMusic()
    {
        isPaused = true;
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        isPaused = false;
        musicSource.UnPause();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void UpdatePreviousVolume()
    {
        previousVolume = musicSource.volume;
    }

    public void Mute()
    {
        musicSource.volume = 0f;
    }

    public void Unmute()
    {
        musicSource.volume = previousVolume;
    }
}
