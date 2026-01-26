using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject prefab; 
    [SerializeField] Transform spawnPoint;

    private float score = 0; 

    public void IncreaseScore()
    {
        score++; 

        if(score == 3)
        {
            Instantiate(prefab, spawnPoint);
        }
    }
}
