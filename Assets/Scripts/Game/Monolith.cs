using System;
using System.Collections;
using UnityEngine;

public class Monolith : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _moveTime = 0.3f;
    [SerializeField] private float _smooth = 15f;
    
    private float _timer;
    
    private void Awake()
    {
        EventBus.PlayerEvents.OnWin += OnWin;
    }
    
    private void OnDestroy()
    {
        EventBus.PlayerEvents.OnWin -= OnWin;
    }

    [ContextMenu("Move")]
    private void OnWin()
    {
        ServiceLocator.Player.transform.SetParent(transform);
        StartCoroutine(MoveTo(new Vector3(0, 1, 0)));
    }
    
    private IEnumerator MoveTo(Vector3 newPosition)
    {
        Transform player = ServiceLocator.Player.transform;
        
        float speed = (player.position - newPosition).magnitude / (_moveTime * _smooth);
        
        while (_timer < _moveTime)
        {
            _timer += Time.deltaTime;
            
            player.position = Vector3.Lerp(player.position, newPosition, Time.deltaTime * speed);
            
            yield return null;
        }
    }
}