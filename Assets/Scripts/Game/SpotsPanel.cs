using System;
using UnityEngine;

public class SpotsPanel : MonoBehaviour, IInteractable
{
    private bool _isActive;
    
    public event Action OnInteract;
    
    public void Interact()
    {
        if (_isActive)
        {
            EventBus.UIEvents.OnSpotsGameWindowShow?.Invoke();
        }
    }

    private void Awake()
    {
        _isActive = true;
        
        EventBus.MiniGamesEvents.OnSpotsGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        EventBus.MiniGamesEvents.OnSpotsGameEnd -= OnGameEnd;
    }

    private void OnGameEnd(bool success)
    {
        if(success)
            _isActive = false;
    }
}