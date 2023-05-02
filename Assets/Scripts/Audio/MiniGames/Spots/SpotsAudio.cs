using UnityEngine;

public class SpotsAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    
    private void Awake()
    {
        EventBus.MiniGamesEvents.OnSpotsGameEnd += OnGameEnd;
    }

    private void OnGameEnd(bool success)
    {
        if (success)
        {
            ServiceLocator.AudioManager.PlaySound(_winClip);
        }
        else
        {
            ServiceLocator.AudioManager.PlaySound(_loseClip);
        }
    }
}