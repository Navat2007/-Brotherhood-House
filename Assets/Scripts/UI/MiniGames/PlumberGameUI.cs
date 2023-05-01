using UnityEngine;

public class PlumberGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    
    private void Awake()
    {
        EventBus.UIEvents.OnPlumberGameWindowShow += OnWindowShow;
        
        _panel.gameObject.SetActive(false);
    }

    private void OnWindowShow()
    {
        _panel.gameObject.SetActive(true);
    }
}