using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Transform _levelPanel;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _pauseButton;
    
    [Header("Мини игры")]
    [SerializeField] private Button _plumberGameButton;
    [SerializeField] private Button _electricGameButton;
    [SerializeField] private Button _spotsGameButton;
    [SerializeField] private Button _pairsGameButton;
    [SerializeField] private Button _plumberWinGameButton;
    [SerializeField] private Button _spotsWinGameButton;
    [SerializeField] private Button _pairsWinGameButton;
    
    [Header("Предметы")]
    [SerializeField] private Button _wheelButton;
    [SerializeField] private Button _flowerButton;
    [SerializeField] private Button _pictureButton;
    
    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            EventBus.PauseEvent?.Invoke();
            EventBus.UIEvents.OnPauseWindowShow?.Invoke();
        });
        
        _plumberGameButton.onClick.AddListener(() =>
        {
            EventBus.UIEvents.OnPlumberGameWindowShow?.Invoke();
        });
        
        _electricGameButton.onClick.AddListener(() =>
        {
            EventBus.UIEvents.OnElectricGameWindowShow?.Invoke();
        });
        
        _spotsGameButton.onClick.AddListener(() =>
        {
            EventBus.UIEvents.OnSpotsGameWindowShow?.Invoke();
        });
        
        _pairsGameButton.onClick.AddListener(() =>
        {
            EventBus.UIEvents.OnPairsGameWindowShow?.Invoke();
        });
        
        _plumberWinGameButton.onClick.AddListener(() =>
        {
            EventBus.MiniGamesEvents.OnPlumberGameEnd?.Invoke(true);
        });
        
        _spotsWinGameButton.onClick.AddListener(() =>
        {
            EventBus.MiniGamesEvents.OnSpotsGameEnd?.Invoke(true);
        });
        
        _pairsWinGameButton.onClick.AddListener(() =>
        {
            EventBus.MiniGamesEvents.OnPairsGameEnd?.Invoke(true);
        });
        
        _wheelButton.onClick.AddListener(() =>
        {
            EventBus.ItemEvents.OnWheelPickUp?.Invoke();
        });
        
        _flowerButton.onClick.AddListener(() =>
        {
            EventBus.ItemEvents.OnFlowerPickUp?.Invoke();
        });
        
        _pictureButton.onClick.AddListener(() =>
        {
            EventBus.ItemEvents.OnPicturePickUp?.Invoke();
        });
        
        EventBus.StartLevelEvent += OnStartLevel;
        EventBus.EndLevelEvent += OnEndLevel;
        EventBus.UIEvents.OnMainMenuWindowShow += OnEndLevel;
        EventBus.UIEvents.OnTimerChanged += OnTimerChanged;
        
        _levelPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventBus.StartLevelEvent -= OnStartLevel;
        EventBus.EndLevelEvent -= OnEndLevel;
        EventBus.UIEvents.OnMainMenuWindowShow -= OnEndLevel;
        EventBus.UIEvents.OnTimerChanged -= OnTimerChanged;
    }
    
    private void OnStartLevel()
    {
        _levelPanel.gameObject.SetActive(true);
    }
    
    private void OnEndLevel()
    {
        _levelPanel.gameObject.SetActive(false);
    }

    private void OnTimerChanged(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        _timerText.text = $"Время: {timeSpan:m\\:ss}";
    }
}