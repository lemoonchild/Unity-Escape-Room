using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance; 
    
    [Header("Spawn Settings")]
    [SerializeField] Transform initialSpawnPoint; 
    private Vector3 currentCheckpoint;
    private Quaternion currentCheckpointRotation;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (initialSpawnPoint != null)
        {
            currentCheckpoint = initialSpawnPoint.position;
            currentCheckpointRotation = initialSpawnPoint.rotation;
            Debug.Log("Initial checkpoint set at: " + currentCheckpoint);
        }
        else
        {
            Debug.LogError("Initial Spawn Point is NULL!");
        }
    }
    
    public void SetCheckpoint(Vector3 position, Quaternion rotation)
    {
        currentCheckpoint = position;
        currentCheckpointRotation = rotation;
        Debug.Log("New checkpoint saved at: " + position);
    }
    
    public void RespawnPlayer(GameObject player)
    {
        if (player != null)
        {
            Debug.Log("Respawning player to: " + currentCheckpoint);
            
            // Manejar Character Controller
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                // Desactivar temporalmente para poder mover
                cc.enabled = false;
                
                // Teleportar
                player.transform.position = currentCheckpoint;
                player.transform.rotation = currentCheckpointRotation;
                
                // Reactivar
                cc.enabled = true;
                
                Debug.Log("Player respawned with Character Controller");
            }
            // Manejar Rigidbody (por si acaso)
            else
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                
                player.transform.position = currentCheckpoint;
                player.transform.rotation = currentCheckpointRotation;
                
                Debug.Log("Player respawned with Rigidbody");
            }
        }
        else
        {
            Debug.LogError("Player GameObject is NULL!");
        }
    }
}