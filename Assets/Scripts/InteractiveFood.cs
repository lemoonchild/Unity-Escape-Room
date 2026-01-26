using UnityEngine;

public class InteractiveFood : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 45f;

    private bool isBeingHeld = false;
    private bool isOnCarpet = false; 
    private Vector3 originalPosition;
    
    [Header("Object Settings")]
    public string objectID;
    
    private void Start()
    {
        originalPosition = transform.position;
    }
    
    void Update()
    {
        if (!isBeingHeld && !isOnCarpet)
        {
            // Rotación
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            
            // Bobbing 
            float bobbingOffset = Mathf.Sin(Time.time * 2f) * 0.05f; 
            transform.position = new Vector3(
                originalPosition.x,
                originalPosition.y + bobbingOffset,
                originalPosition.z
            );
        }
        else if (isOnCarpet)
        {
            transform.position = originalPosition;
        }
    }
    
    public void OnPickedUp()
    {
        isBeingHeld = true;
    }
    
    public void OnDropped()
    {
        isBeingHeld = false;
        originalPosition = transform.position;
    }

    public void OnPlacedOnCarpet() 
    {
        isOnCarpet = true;
        isBeingHeld = false;
        originalPosition = transform.position;
    }

    public void OnRemovedFromCarpet() 
    {
        isOnCarpet = false;
        originalPosition = transform.position;
    }
}