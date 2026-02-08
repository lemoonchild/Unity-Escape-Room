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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}