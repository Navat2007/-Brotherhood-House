using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Transform _gameOverPanel;
    [Space] 
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _leaderBoardButton;
    [Space] 
    [SerializeField] private TMP_Text _appleCountText;
    [SerializeField] private TMP_Text _maxAppleCountText;

    [Header("Окно ввода имени")] 
    [SerializeField] private Transform _nameInputWindowPanel;

    [SerializeField] private Button _closeInputWindowButton;
    [SerializeField] private Button _acceptInputWindowButton;
    [SerializeField] private TMP_Text _acceptInputWindowButtonAppleText;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_Text _errorInputWindowText;

    [Header("Окно отправки")] 
    [SerializeField] private Transform _sendingPanel;

    private void Awake()
    {
        // _closeButton.onClick.AddListener(() =>
        // {
        //     _gameOverPanel.gameObject.SetActive(false);
        //     EventBus.UIEvents.OnMainMenuWindowShow?.Invoke();
        // });
        //
        // _restartButton.onClick.AddListener(() =>
        // {
        //     _gameOverPanel.gameObject.SetActive(false);
        //     EventBus.StartLevelEvent.Invoke();
        // });
        //
        // _leaderBoardButton.onClick.AddListener(() => { EventBus.UIEvents.OnLeaderBoardWindowShow?.Invoke(); });

        EventBus.EndLevelEvent += OnGameOverWindowShow;
        EventBus.UIEvents.OnGameOverWindowShow += OnGameOverWindowShow;

        // _gameOverPanel.gameObject.SetActive(false);
        // _nameInputWindowPanel.gameObject.SetActive(false);
        // _sendingPanel.gameObject.SetActive(false);
    }

    private void OnGameOverWindowShow()
    {
        
    }
}