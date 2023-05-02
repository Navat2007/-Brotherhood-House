using UnityEngine;
using UnityEngine.UI;

public class ElectricGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _closeButton;
    
    private void Awake()
    {
        EventBus.UIEvents.OnElectricGameWindowShow += OnWindowShow;
        EventBus.StartLevelEvent += OnWindowHide;
        
        _closeButton.onClick.AddListener(() =>
        {
            OnWindowHide();
            EventBus.MiniGamesEvents.OnMiniGameEnd?.Invoke();
            EventBus.MiniGamesEvents.OnElectricGameEnd?.Invoke(false);
        });
        
        OnWindowHide();
    }

    private void OnWindowShow()
    {
        _panel.gameObject.SetActive(true);
        EventBus.MiniGamesEvents.OnMiniGameStart?.Invoke();
        EventBus.MiniGamesEvents.OnElectricGameStart?.Invoke();
    }
    
    private void OnWindowHide()
    {
        _panel.gameObject.SetActive(false);
    }
}