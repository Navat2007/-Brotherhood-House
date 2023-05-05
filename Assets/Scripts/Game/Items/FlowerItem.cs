using System;
using UnityEngine;

public class FlowerItem : MonoBehaviour, IInteractable
{
    public event Action OnInteract;
    
    public void Interact()
    {
        EventBus.ItemEvents.OnFlowerPickUp?.Invoke();
        Destroy(gameObject);
    }
}