using UnityEngine;
using UnityEngine.InputSystem;

public class WinMenu : MonoBehaviour
{
    public GameObject WinContainer;

    public void MainMenuBtn(string levelName)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void QuitGame(){
        
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
