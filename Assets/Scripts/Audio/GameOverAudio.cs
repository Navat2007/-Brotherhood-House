using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _gameOverClip;
    
    private void Awake()
    {
        EventBus.PlayerEvents.OnDeath += OnGameOver;
    }
    
    private void OnDestroy()
    {
        EventBus.PlayerEvents.OnDeath -= OnGameOver;
    }

    private void OnGameOver()
    {
        ServiceLocator.AudioManager.PlaySound(_gameOverClip);
    }
}