using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Transform _levelPanel;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _pauseButton;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            EventBus.PauseEvent?.Invoke();
            EventBus.UIEvents.OnPauseWindowShow?.Invoke();
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