using System;
using UnityEngine;

public class PlumberPanel : MonoBehaviour, IInteractable
{
    private bool _isActive;
    
    public event Action OnInteract;
    
    public void Interact()
    {
        Debug.Log("Interact");
        if (_isActive)
        {
            EventBus.UIEvents.OnPlumberGameWindowShow?.Invoke();
        }
    }

    private void Awake()
    {
        _isActive = true;
        
        EventBus.MiniGamesEvents.OnPlumberGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        EventBus.MiniGamesEvents.OnPlumberGameEnd -= OnGameEnd;
    }

    private void OnGameEnd(bool success)
    {
        if(success)
            _isActive = false;
    }
}