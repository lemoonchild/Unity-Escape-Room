using UnityEngine;
using UnityEngine.InputSystem;

public class WinMenu : MonoBehaviour
{
    public GameObject WinContainer;
    
    [Header("Audio")]
    [SerializeField] AudioClip buttonClickSound; 
    [SerializeField][Range(0f, 1f)] float volume = 0.5f;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.ignoreListenerPause = true; 
    }
    
    public void MainMenuBtn(string levelName)
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
    
    public void QuitGame()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}