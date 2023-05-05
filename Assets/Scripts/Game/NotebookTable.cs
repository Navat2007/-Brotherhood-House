using System;
using UnityEngine;

public class NotebookTable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _ui;
    
    public event Action OnInteract;
    
    public void Interact()
    {
        if (ServiceLocator.Player.IsHaveItem)
        {
            EventBus.PlayerEvents.OnWin?.Invoke();
            Destroy(_ui.gameObject);
        }
    }
}