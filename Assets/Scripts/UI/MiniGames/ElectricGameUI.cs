using UnityEngine;

public class ElectricGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    
    private void Awake()
    {
        EventBus.UIEvents.OnElectricGameWindowShow += OnWindowShow;
        EventBus.StartLevelEvent += OnWindowHide;
        
        _panel.gameObject.SetActive(false);
    }

    private void OnWindowShow()
    {
        _panel.gameObject.SetActive(true);
    }
    
    private void OnWindowHide()
    {
        _panel.gameObject.SetActive(false);
    }
}