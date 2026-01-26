using UnityEngine;
using UnityEngine.InputSystem;  

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] float detectionRadius = 3f;
    [SerializeField] Transform holdPosition;
    
    [Header("Detection Settings")]
    [SerializeField] LayerMask interactableLayer;
    
    private InteractiveFood closestInteractable;
    private GameObject heldObject;
    private Rigidbody heldObjectRb;
    
    private void Update()
    {
        if (heldObject == null)
        {
            DetectClosestInteractable();
        }

        else
        {
            if (closestInteractable != null)
            {
                InteractionPrompt prompt = closestInteractable.GetComponent<InteractionPrompt>();
                if (prompt != null)
                {
                    prompt.HidePrompt();
                }
                closestInteractable = null;
            }
        }
        
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }
    }
    
    private void DetectClosestInteractable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, interactableLayer);
        
        InteractiveFood nearest = null;
        float nearestDistance = float.MaxValue;
        
        foreach (Collider col in hitColliders)
        {
            if (heldObject != null && col.gameObject == heldObject)
            {
                continue; 
            }

            InteractiveFood interactable = col.GetComponent<InteractiveFood>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = interactable;
                }
            }
        }
        
        if (nearest != closestInteractable)
        {
            if (closestInteractable != null)
            {
                InteractionPrompt oldPrompt = closestInteractable.GetComponent<InteractionPrompt>();
                if (oldPrompt != null)
                {
                    oldPrompt.HidePrompt();
                }
            }
            
            closestInteractable = nearest;
            
            if (closestInteractable != null)
            {
                InteractionPrompt newPrompt = closestInteractable.GetComponent<InteractionPrompt>();
                if (newPrompt != null)
                {
                    newPrompt.ShowPrompt();
                }
            }
        }
    }
    
    private void TryPickupObject()
    {
        if (closestInteractable != null)
        {
            heldObject = closestInteractable.gameObject;
            heldObjectRb = heldObject.GetComponent<Rigidbody>();
            
            if (heldObjectRb != null)
            {
                heldObjectRb.isKinematic = true;
                heldObjectRb.useGravity = false;
                heldObjectRb.linearVelocity = Vector3.zero; 
                heldObjectRb.angularVelocity = Vector3.zero; 
            }
            
            Collider objCollider = heldObject.GetComponent<Collider>();
            if (objCollider != null)
            {
                objCollider.enabled = false;
            }

            InteractionPrompt prompt = closestInteractable.GetComponent<InteractionPrompt>();
            if (prompt != null)
            {
                prompt.HidePrompt();
            }
            
            closestInteractable.OnPickedUp();
            heldObject.transform.SetParent(holdPosition);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;

            closestInteractable = null;
        }
    }
    
    private void DropObject()
    {
        if (heldObject != null)
        {
            GameObject droppedObject = heldObject;

            heldObject.transform.SetParent(null);
            
            if (heldObjectRb != null)
            {
                heldObjectRb.isKinematic = false;
                heldObjectRb.useGravity = true;
                heldObjectRb.linearVelocity = Vector3.zero; 
            }
            
            Collider objCollider = heldObject.GetComponent<Collider>();
            if (objCollider != null)
            {
                StartCoroutine(EnableColliderDelayed(objCollider));
            }
            
            InteractiveFood peach = heldObject.GetComponent<InteractiveFood>();
            if (peach != null)
            {
                peach.OnDropped();
            }
            
            heldObject = null;
            heldObjectRb = null;

            StartCoroutine(HideDroppedObjectPrompt(droppedObject));
        }
    }
    
    private System.Collections.IEnumerator EnableColliderDelayed(Collider col)
    {
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
    }

    private System.Collections.IEnumerator HideDroppedObjectPrompt(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f); 
        
        if (obj != null)
        {
            InteractionPrompt prompt = obj.GetComponent<InteractionPrompt>();
            if (prompt != null && closestInteractable != null && closestInteractable.gameObject != obj)
            {
                prompt.HidePrompt();
            }
        }
    }
}