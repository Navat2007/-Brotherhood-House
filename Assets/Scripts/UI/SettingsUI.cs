using System;
using Bayat.SaveSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    private const float MinVolume = 0.0001f;
    private const string MusicVolumeKey = "Music";
    private const string EffectsVolumeKey = "Effects";
    
    [SerializeField] private Transform _settingsPanel;
    [SerializeField] private Button _closeButton;
    
    [Header("Music")] 
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Image _musicIcon;
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;

    [Header("Effects")] 
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Image _effectsIcon;
    [SerializeField] private Sprite _effectsOnSprite;
    [SerializeField] private Sprite _effectsOffSprite;

    private void Awake()
    {
        _closeButton.onClick.AddListener(() =>
        {
            _settingsPanel.gameObject.SetActive(false);
        });
        
        _musicSlider.onValueChanged.AddListener((value) =>
        {
            ServiceLocator.AudioManager.SetVolume(value, AudioManager.AudioType.Music);

            _musicIcon.sprite = value > MinVolume ? _musicOnSprite : _musicOffSprite;
        });

        _effectsSlider.onValueChanged.AddListener((value) =>
        {
            ServiceLocator.AudioManager.SetVolume(value, AudioManager.AudioType.Effects);

            _effectsIcon.sprite = value > MinVolume ? _effectsOnSprite : _effectsOffSprite;
        });
        
        EventBus.UIEvents.OnSettingsWindowShow += OnSettingsWindowShow;
        
        _settingsPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventBus.UIEvents.OnSettingsWindowShow -= OnSettingsWindowShow;
    }

    private async void Start()
    {
        const float defaultSoundVolume = 0.5f;
        const float defaultMusicVolume = 0.05f;
        
        if (await SaveSystemAPI.ExistsAsync(EffectsVolumeKey))
        {
            _effectsSlider.value = await SaveSystemAPI.LoadAsync<float>(EffectsVolumeKey);
        }
        else
        {
            _effectsSlider.value = defaultSoundVolume;
        }
        
        if (await SaveSystemAPI.ExistsAsync(MusicVolumeKey))
        {
            _musicSlider.value = await SaveSystemAPI.LoadAsync<float>(MusicVolumeKey);
        }
        else
        {
            _musicSlider.value = defaultMusicVolume;
        }
        
        _settingsPanel.gameObject.SetActive(false);
    }

    private void OnSettingsWindowShow()
    {
        _settingsPanel.gameObject.SetActive(true);
    }
}