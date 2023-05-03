using System;
using UnityEngine;

public class PlumberGame : MonoBehaviour
{
    [SerializeField] private Transform _plumberBox;
    [SerializeField] private SpotItem _pipeItemPrefab;
    [SerializeField] private int _secondsToLoose = 30;
    
    private bool _isGameRunning = false;
    private float _timer = 1000;
    
    public event Action<float, bool> OnTimerChange;
    
    private void Awake()
    {
        ServiceLocator.PlumberGame = this;

        EventBus.MiniGamesEvents.OnPlumberGameStart += OnGameStart;
        EventBus.MiniGamesEvents.OnPlumberGameEnd += OnGameEnd;
    }
    
    private void OnGameStart()
    {
        
    }

    private void OnGameEnd(bool success)
    {
        
    }
}