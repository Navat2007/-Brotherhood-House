using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGameUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(LoadScene);

        EventBus.EndLevelEvent += OnWinGameWindowShow;
        
        _panel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(LoadScene);
        
        EventBus.PlayerEvents.OnDeath -= OnWinGameWindowShow;
    }

    private void OnWinGameWindowShow()
    {
        _panel.gameObject.SetActive(true);
    }
    
    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}