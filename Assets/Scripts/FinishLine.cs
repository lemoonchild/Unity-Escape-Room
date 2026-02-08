using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header("Win Settings")]
    [SerializeField] GameObject winScreen;
    [SerializeField] AudioSource backgroundMusic; 
    
    [Header("Audio")]
    [SerializeField] AudioClip winSound;
    [SerializeField][Range(0f, 1f)] float volume = 0.8f;
    
    private bool hasWon = false;
    private AudioSource winAudioSource;
    
    private void Start()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
        
        winAudioSource = gameObject.AddComponent<AudioSource>();
        winAudioSource.playOnAwake = false;
        winAudioSource.volume = volume;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;            
            ShowWinScreen();
        }
    }
    
    private void ShowWinScreen()
    {
        if (winScreen != null)
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
            
            if (winSound != null && winAudioSource != null)
            {
                winAudioSource.PlayOneShot(winSound);
            }
            
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}