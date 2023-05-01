using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _currentTime;
    
    public float CurrentTime => _currentTime;

    private void Awake()
    {
        ServiceLocator.Timer = this;
    }
    
    private void Start()
    {
        EventBus.StartLevelEvent += Reset;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        EventBus.UIEvents.OnTimerChanged?.Invoke(_currentTime);
    }

    private void Reset()
    {
        _currentTime = 0;
    }
}