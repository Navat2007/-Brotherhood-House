using System;
using UnityEngine;

public class PictureItem : MonoBehaviour, IInteractable
{
    public event Action OnInteract;
    
    public void Interact()
    {
        EventBus.ItemEvents.OnPicturePickUp?.Invoke();
        Destroy(gameObject);
    }
}