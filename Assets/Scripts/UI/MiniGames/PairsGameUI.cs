using UnityEngine;
using UnityEngine.UI;

public class PairsGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _closeButton;
    
    private void Awake()
    {
        EventBus.UIEvents.OnPairsGameWindowShow += OnWindowShow;
        EventBus.StartLevelEvent += OnWindowHide;
        
        _closeButton.onClick.AddListener(() =>
        {
            OnWindowHide();
            EventBus.MiniGamesEvents.OnMiniGameEnd?.Invoke();
            EventBus.MiniGamesEvents.OnPairsGameEnd?.Invoke(false);
        });
        
        OnWindowHide();
    }

    private void OnWindowShow()
    {
        _panel.gameObject.SetActive(true);
        EventBus.MiniGamesEvents.OnMiniGameStart?.Invoke();
        EventBus.MiniGamesEvents.OnPairsGameStart?.Invoke();
    }
    
    private void OnWindowHide()
    {
        _panel.gameObject.SetActive(false);
    }
}