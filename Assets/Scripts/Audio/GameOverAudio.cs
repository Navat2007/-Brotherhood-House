using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _gameOverClip;
    
    private void Awake()
    {
        EventBus.EndLevelEvent += OnGameOver;
    }
    
    private void OnDestroy()
    {
        EventBus.EndLevelEvent -= OnGameOver;
    }

    private void OnGameOver()
    {
        ServiceLocator.AudioManager.PlaySound(_gameOverClip);
    }
}