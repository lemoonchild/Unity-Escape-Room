using UnityEngine;

public class TomatoesHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.RespawnPlayer(other.gameObject);
            }
        }
    }
}