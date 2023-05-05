using System;
using UnityEngine;

public class WheelItem : MonoBehaviour, IInteractable
{
    public event Action OnInteract;
    
    public void Interact()
    {
        EventBus.ItemEvents.OnWheelPickUp?.Invoke();
        Destroy(gameObject);
    }
}