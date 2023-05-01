using UnityEngine;

public class SpotsGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    
    private void Awake()
    {
        EventBus.UIEvents.OnSpotsGameWindowShow += OnWindowShow;
        
        _panel.gameObject.SetActive(false);
    }

    private void OnWindowShow()
    {
        _panel.gameObject.SetActive(true);
    }
}