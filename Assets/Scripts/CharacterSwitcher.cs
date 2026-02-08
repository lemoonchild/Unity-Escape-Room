using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("Character Prefab")]
    [SerializeField] GameObject newCharacterPrefab;
    
    [Header("Current Character")]
    [SerializeField] GameObject currentCharacter;
    
    public void SpawnNewCharacter()
    {
        if (currentCharacter != null && newCharacterPrefab != null)
        {
            Vector3 spawnPos = currentCharacter.transform.position;
            Quaternion spawnRot = currentCharacter.transform.rotation;
            
            Destroy(currentCharacter);
            
            GameObject newChar = Instantiate(newCharacterPrefab, spawnPos, spawnRot);
        }
    }
}