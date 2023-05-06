using UnityEngine;
using UnityEngine.UI;

public class LetterUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _closeButton;
    
    private void Awake()
    {
        _closeButton.onClick.AddListener(() =>
        {
            _panel.gameObject.SetActive(false);
        });
        
        EventBus.StartLevelEvent += OnStart;
        
        _panel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventBus.UIEvents.OnSettingsWindowShow -= OnStart;
    }
    
    private void OnStart()
    {
        _panel.gameObject.SetActive(true);
    }
}