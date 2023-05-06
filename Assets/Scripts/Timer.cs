using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _maxTime;
    [SerializeField] private float _loseMiniGameTime = 60;
    
    private float _currentTime;
    
    public float MaxTime => _maxTime;
    public float CurrentTime => _currentTime;

    private void Awake()
    {
        ServiceLocator.Timer = this;
        
        EventBus.StartLevelEvent += Reset;
        EventBus.MiniGamesEvents.OnPlumberGameEnd += SubstractTime;
        EventBus.MiniGamesEvents.OnSpotsGameEnd += SubstractTime;
        EventBus.MiniGamesEvents.OnPairsGameEnd += SubstractTime;
          

        _currentTime = _maxTime;
    }

    private void OnDestroy()
    {
        EventBus.StartLevelEvent -= Reset;
        EventBus.MiniGamesEvents.OnPlumberGameEnd -= SubstractTime;
        EventBus.MiniGamesEvents.OnSpotsGameEnd -= SubstractTime;
        EventBus.MiniGamesEvents.OnPairsGameEnd -= SubstractTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        EventBus.UIEvents.OnTimerChanged?.Invoke(_currentTime);
        
        if(_currentTime <= 0)
            EventBus.PlayerEvents.OnDeath?.Invoke();
    }

    private void Reset()
    {
        _currentTime = _maxTime;
    }
    
    private void SubstractTime(bool success)
    {
        if(success == false)
        {
            _currentTime -= _loseMiniGameTime;
        }
    }
}