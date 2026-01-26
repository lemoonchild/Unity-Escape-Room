using UnityEngine;
using System.Collections.Generic;

public class FolderReceptor : MonoBehaviour
{
    [Header("Required Objects")]
    [SerializeField] List<string> requiredObjectIDs = new List<string>();
    private HashSet<string> placedObjectIDs = new HashSet<string>();
    
    [Header("Island Settings")]
    [SerializeField] GameObject island;

    [Header("Character Spawner")] 
    [SerializeField] CharacterSpawner characterSpawner;

    private bool hasSpawned = false;
    
    private void Start()
    {
        if (island != null)
        {
            island.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {        
        InteractiveFood interactable = other.GetComponent<InteractiveFood>();
        
        if (interactable == null)
        {
            return; 
        }
        
        if (requiredObjectIDs.Contains(interactable.objectID))
        {
            if (!placedObjectIDs.Contains(interactable.objectID))
            {
                placedObjectIDs.Add(interactable.objectID);
                interactable.OnPlacedOnCarpet();
                CheckCompletion();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        InteractiveFood interactable = other.GetComponent<InteractiveFood>();
        
        if (interactable != null && placedObjectIDs.Contains(interactable.objectID))
        {
            placedObjectIDs.Remove(interactable.objectID);
            interactable.OnRemovedFromCarpet();

            hasSpawned = false;
            
            if (island != null && island.activeSelf)
            {
                island.SetActive(false);
            }
        }
    }
    
    private void CheckCompletion()
    {
        if (placedObjectIDs.Count == requiredObjectIDs.Count)
        {
            if (island != null)
            {
                island.SetActive(true);
            }

            if (!hasSpawned && characterSpawner != null)
            {
                hasSpawned = true;
                characterSpawner.SpawnNewCharacter();
            }
        }
    }

    public bool IsCompleted()
    {
        return placedObjectIDs.Count == requiredObjectIDs.Count;
    }
}