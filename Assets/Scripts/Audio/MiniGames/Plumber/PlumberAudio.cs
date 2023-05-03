using UnityEngine;

public class PlumberAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    
    private void Awake()
    {
        EventBus.MiniGamesEvents.OnPlumberGameEnd += OnGameEnd;
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