using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseContainer;
    private bool isPaused = false;
    
    [Header("Audio")]
    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] AudioClip pauseMusic; 
    [SerializeField][Range(0f, 1f)] float sfxVolume = 0.5f;
    [SerializeField][Range(0f, 1f)] float musicVolume = 0.3f;
    
    [Header("Background Music Reference")]
    [SerializeField] AudioSource gameBackgroundMusic; 
    
    private AudioSource sfxSource; 
    private AudioSource musicSource; 
    
    void Start()
    {
        PauseContainer.SetActive(false);
        
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.clip = pauseMusic;
    }
    
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    
    void PauseGame()
    {
        isPaused = true;
        PauseContainer.SetActive(true);
        Time.timeScale = 0;
        
        if (gameBackgroundMusic != null)
        {
            gameBackgroundMusic.Pause();
        }
        
        if (pauseMusic != null && musicSource != null)
        {
            musicSource.Play();
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    void ResumeGame()
    {
        isPaused = false;
        PauseContainer.SetActive(false);
        Time.timeScale = 1;
        
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        
        if (gameBackgroundMusic != null)
        {
            gameBackgroundMusic.UnPause();
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ResumeGameBtn()
    {
        if (buttonClickSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
        ResumeGame();
    }
    
    public void MainMenuBtn(string levelName)
    {
        if (buttonClickSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
        
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}