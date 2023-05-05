using System;
using UnityEngine;

public class NotebookTable : MonoBehaviour, IInteractable
{
    public event Action OnInteract;
    
    public void Interact()
    {
        if (ServiceLocator.Player.IsHaveItem)
        {
            EventBus.EndLevelEvent?.Invoke();
        }
    }
}