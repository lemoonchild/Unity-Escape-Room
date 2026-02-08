using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Manager References")]
    [SerializeField] LevelManager levelManager; 
    [SerializeField] LevelManagerOpenDoor openDoorManager; 
    
    [Header("Audio")]
    [SerializeField] AudioClip pickupSound; 

    private AudioSource audioSource;

    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
        
        float bobbing = Mathf.Sin(Time.time * 2f) * 0.2f;
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + bobbing * Time.deltaTime,
            transform.position.z
        );
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.2f);

            if (levelManager != null)
            {
                levelManager.IncreaseScore();
            }
            
            if (openDoorManager != null)
            {
                openDoorManager.IncreaseScore();
            }
            
            Destroy(this.gameObject);
        }
    }
}