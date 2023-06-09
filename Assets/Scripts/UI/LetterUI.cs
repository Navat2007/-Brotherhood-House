using System;
using UnityEngine;
using UnityEngine.UI;

public class LetterUI : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private float _timeToShow = 1f;
    [SerializeField] private AudioClip _audioClip;
    
    private float _timer;
    private bool _isOpen;
    
    private void Awake()
    {
        _closeButton.onClick.AddListener(OnClickCloseButton);
        
        _panel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnClickCloseButton);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _timeToShow && _isOpen == false)
        {
            _isOpen = true;
            ServiceLocator.AudioManager.PlaySound(_audioClip);
            _panel.gameObject.SetActive(true);
            EventBus.PauseEvent?.Invoke();
        }
    }

    private void OnClickCloseButton()
    {
        EventBus.UnPauseEvent?.Invoke();
        _panel.gameObject.SetActive(false);
    }
}