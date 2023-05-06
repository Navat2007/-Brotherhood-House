using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Monolith : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _timeToGameWin = 3f;
    [SerializeField] private float _moveTime = 0.3f;
    [SerializeField] private float _shakeTime = 2f;
    [SerializeField] private float _shrinkSpeed = 1f;
    [SerializeField] private Transform _eye;
    [SerializeField] private SpriteRenderer _eyeSpriteRenderer;
    [SerializeField] private float _eyeShowSpeed = 2f;
    [SerializeField] private List<Transform> _objectsToHide = new List<Transform>();

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
        foreach (var item in _objectsToHide)
        {
            item.gameObject.SetActive(false);
        }
        
        ServiceLocator.Player.transform.SetParent(transform);
        StartCoroutine(MoveTo(new Vector3(0, 1.35f, 0)));
    }
    
    private IEnumerator MoveTo(Vector3 newPosition)
    {
        _timer = 0;
        Transform player = ServiceLocator.Player.transform;
        Vector3 originPosition = player.transform.localPosition;

        while (Vector3.Distance(player.transform.localPosition, newPosition) > 0)
        {
            _timer += Time.deltaTime;
            
            player.transform.localPosition = Vector3.Lerp(originPosition, newPosition, _timer / _moveTime);
            
            yield return null;
        }
        
        yield return StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        _timer = 0;
        Transform player = ServiceLocator.Player.transform;
        
        while (player.localScale.x > 0.1f)
        {
            _timer += Time.deltaTime;
            
            player.localScale = Vector3.Lerp(player.localScale, Vector3.zero, Time.deltaTime * _shrinkSpeed);
            
            yield return null;
        }
        
        player.gameObject.SetActive(false);
        
        yield return StartCoroutine(Shake());
    }
    
    private IEnumerator Shake()
    {
        _timer = 0;

        CinemachineBasicMultiChannelPerlin _perlin =
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _perlin.m_AmplitudeGain = 3;
        
        while (_timer < _shakeTime)
        {
            _timer += Time.deltaTime;
            
            yield return null;
        }
        
        _perlin.m_AmplitudeGain = 0;
        
        yield return StartCoroutine(ShowEye());
    }
    
    private IEnumerator ShowEye()
    {
        _eye.gameObject.SetActive(true);
        _timer = 0;
        
        while (_eyeSpriteRenderer.color.a < 1)
        {
            Debug.Log(_eyeSpriteRenderer.color.a);
            _timer += Time.deltaTime;
            
            float alpha = Mathf.Lerp(0, 30, _timer / _eyeShowSpeed);
            _eyeSpriteRenderer.color = new Color(_eyeSpriteRenderer.color.r, _eyeSpriteRenderer.color.g, _eyeSpriteRenderer.color.b, alpha);
            
            yield return null;
        }
        
        StartCoroutine(WinGame());
    }
    
    private IEnumerator WinGame()
    {
        Debug.Log("Win");
        yield return new WaitForSeconds(_timeToGameWin);
        
        EventBus.EndLevelEvent?.Invoke();
    }
}