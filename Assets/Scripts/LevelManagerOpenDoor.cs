using UnityEngine;

public class LevelManagerOpenDoor : MonoBehaviour
{

    [Header("Destroy Settings")]
    [SerializeField] GameObject blockToRemove;

    private float score = 0; 

    public void IncreaseScore()
    {
        score++; 

        if(score == 1)
        {
            Destroy(blockToRemove); 
        }
    }
}
