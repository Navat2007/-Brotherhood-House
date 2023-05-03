using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Transform _gameOverPanel;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(LoadScene);

        EventBus.PlayerEvents.OnDeath += OnGameOverWindowShow;
        
        _gameOverPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(LoadScene);
        
        EventBus.PlayerEvents.OnDeath -= OnGameOverWindowShow;
    }

    private void OnGameOverWindowShow()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}