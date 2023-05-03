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
        
        _closeButton.onClick.AddListener(() =>
        {
            OnWindowHide();
            EventBus.MiniGamesEvents.OnMiniGameEnd?.Invoke();
            EventBus.MiniGamesEvents.OnPlumberGameEnd?.Invoke(false);
        });
        
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
    }
}