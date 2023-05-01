using System;
using UnityEngine.Events;

public interface IInteractable
{
    public event Action OnInteract;
    
    public void Interact();
}