using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlumberGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _winPanel;
    [SerializeField] private Transform _losePanel;
    [SerializeField] private float _timeToClose = 1.5f;
    [SerializeField] private TMP_Text _timerText;
    
    private void Awake()
    {
        EventBus.UIEvents.OnPlumberGameWindowShow += OnWindowShow;
        EventBus.StartLevelEvent += OnWindowHide;
        EventBus.MiniGamesEvents.OnPlumberGameEnd += OnGameEnd;
        
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        
        OnWindowHide();
    }
    
    private void OnDestroy()
    {
        EventBus.UIEvents.OnPlumberGameWindowShow -= OnWindowShow;
        EventBus.StartLevelEvent -= OnWindowHide;
        EventBus.MiniGamesEvents.OnPlumberGameEnd -= OnGameEnd;

        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }
    
    private void OnCloseButtonClick()
    {
        if(ServiceLocator.PlumberGame.IsGameRunning)
            EventBus.MiniGamesEvents.OnPlumberGameEnd?.Invoke(false);
    }
    
    private void Start()
    {
        ServiceLocator.PlumberGame.OnTimerChange += OnTimerChange;
    }
    
    private void OnTimerChange(float time, bool isLowTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        _timerText.text = $"{(int)timeSpan.TotalSeconds}";
        
        if(isLowTime)
            _timerText.color = Color.red;
        else
            _timerText.color = Color.blue;
    }
    
    private void OnGameEnd(bool success)
    {
        if(success)
        {
            _winPanel.gameObject.SetActive(true);
        }
        else
        {
            _losePanel.gameObject.SetActive(true);
        }

        StartCoroutine(FadeWindow());
    }
    
    private IEnumerator FadeWindow()
    {
        yield return new WaitForSeconds(_timeToClose);
        
        EventBus.MiniGamesEvents.OnMiniGameEnd?.Invoke();
        OnWindowHide();
    }

    private void OnWindowShow()
    {
        _panel.gameObject.SetActive(true);
        EventBus.MiniGamesEvents.OnMiniGameStart?.Invoke();
        EventBus.MiniGamesEvents.OnPlumberGameStart?.Invoke();
    }
    
    private void OnWindowHide()
    {
        _panel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);
        _winPanel.gameObject.SetActive(false);
    }
}