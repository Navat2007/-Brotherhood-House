using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _state = GameState.PAUSE;
    
    public enum GameState
    {
        PLAY,
        PAUSE
    }
    
    public GameState GetState => _state;

    private void Awake()
    {
        ServiceLocator.GameManager = this;
        Time.timeScale = 0;
        
        EventBus.PauseEvent += OnPause;
        EventBus.UnPauseEvent += OnResume;
        EventBus.StartLevelEvent += OnResume;
        EventBus.EndLevelEvent += OnPause;
    }

    private void OnDestroy()
    {
        EventBus.PauseEvent -= OnPause;
        EventBus.UnPauseEvent -= OnResume;
        EventBus.StartLevelEvent -= OnResume;
        EventBus.EndLevelEvent -= OnPause;
    }

    private void Start()
    {
        Application.targetFrameRate = -1;
        
        ServiceLocator.CheckServices();
    }

    private void OnResume()
    {
        Time.timeScale = 1;
        _state = GameState.PLAY;
    }

    private void OnPause()
    {
        Time.timeScale = 0;
        _state = GameState.PAUSE;
    }
}