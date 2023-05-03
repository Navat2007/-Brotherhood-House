using UnityEngine;

public class PairsAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    
    private void Awake()
    {
        EventBus.MiniGamesEvents.OnPairsGameEnd += OnGameEnd;
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