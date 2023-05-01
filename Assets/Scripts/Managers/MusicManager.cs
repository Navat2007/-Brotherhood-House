using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        ServiceLocator.MusicManager = this;
    }

    private void MuteMusic()
    {
        _audioSource.mute = true;
    }
    
    private void UnmuteMusic()
    {
        _audioSource.mute = false;
    }
}