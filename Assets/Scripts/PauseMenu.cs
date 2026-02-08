using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseContainer;
    private bool isPaused = false;

    void Start()
    {
        PauseContainer.SetActive(false);
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
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    void ResumeGame()
    {
        isPaused = false;
        PauseContainer.SetActive(false);
        Time.timeScale = 1;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
    }
    
    public void ResumeGameBtn()
    {
        ResumeGame();
    }

    public void MainMenuBtn(string levelName)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}