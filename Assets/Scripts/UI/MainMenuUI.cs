using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Transform _menuPanel;
    [Space]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    
    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            EventBus.StartLevelEvent?.Invoke();
        });
        
        _optionsButton.onClick.AddListener(() =>
        {
            EventBus.UIEvents.OnSettingsWindowShow?.Invoke();
        });

        EventBus.StartLevelEvent += OnStartLevel;
        EventBus.UIEvents.OnMainMenuWindowShow += OnMainMenuWindowShow;
        
        _menuPanel.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventBus.StartLevelEvent -= OnStartLevel;
        EventBus.UIEvents.OnMainMenuWindowShow -= OnMainMenuWindowShow;
    }

    private void OnStartLevel()
    {
        _menuPanel.gameObject.SetActive(false);
    }
    
    private void OnMainMenuWindowShow()
    {
        _menuPanel.gameObject.SetActive(true);
    }
}