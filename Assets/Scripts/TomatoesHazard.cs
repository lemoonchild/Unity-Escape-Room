using UnityEngine;

public class TomatoesHazard : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] AudioClip hitSound;
    [SerializeField][Range(0f, 1f)] float volume = 0.7f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, volume);
            }

            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.RespawnPlayer(other.gameObject);
            }
        }
    }
}