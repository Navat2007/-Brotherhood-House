using System;
using UnityEngine;

public class PairsPanel : MonoBehaviour, IInteractable
{
    private bool _isActive;
    
    public event Action OnInteract;
    
    public void Interact()
    {
        if (_isActive)
        {
            EventBus.UIEvents.OnPairsGameWindowShow?.Invoke();
        }
    }

    private void Awake()
    {
        _isActive = true;
        
        EventBus.MiniGamesEvents.OnPairsGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        EventBus.MiniGamesEvents.OnPairsGameEnd -= OnGameEnd;
    }

    private void OnGameEnd(bool success)
    {
        if(success)
            _isActive = false;
    }
}