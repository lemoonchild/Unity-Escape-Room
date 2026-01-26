using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header("Win Settings")]
    [SerializeField] GameObject winScreen;
    
    private bool hasWon = false;
    
    private void Start()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
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
            winScreen.SetActive(true);
            
            Time.timeScale = 0f;
        }
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
    
    public void QuitGame()
    {
        Time.timeScale = 1f;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}