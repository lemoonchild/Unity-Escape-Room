using UnityEngine;
using TMPro; 

public class InteractionPrompt : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] TextMeshPro promptText; 
    [SerializeField] float heightOffset = 0.5f; 
    
    private Transform playerCamera;
    
    private void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
            
            promptText.transform.localPosition = new Vector3(0, heightOffset, 0);
        }
    }
    
    private void Update()
    {
        // Rotar hacia la cámara si está activo
        if (promptText != null && promptText.gameObject.activeSelf)
        {
            if (playerCamera == null)
            {
                playerCamera = Camera.main.transform;
            }
            
            if (playerCamera != null)
            {
                promptText.transform.LookAt(playerCamera);
                promptText.transform.Rotate(0, 180, 0); 
            }
        }
    }
    
    public void ShowPrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            playerCamera = Camera.main.transform;
        }
    }
    
    public void HidePrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}