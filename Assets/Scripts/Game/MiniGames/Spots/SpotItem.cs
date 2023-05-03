using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpotItem : MonoBehaviour
{
    [SerializeField] private float _moveTime = 0.3f;
    [SerializeField] private float _smooth = 15f;
    [SerializeField] private TMP_Text _text;
    
    private RectTransform _rectTransform;
    private Button _button;
    
    private Vector2 _previousPosition;
    private int _number;
    private int _x;
    private int _y;
    private bool _readyToMove = true;
    private float _timer;
    
    public event Action<Vector2, Vector2, SpotItem> OnMoveEnd;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void SetNumber(int number)
    {
        _number = number;
        _text.SetText(number.ToString());
    }

    public void SetPosition(Vector3 position)
    {
        _rectTransform.localPosition = position;
    }
    
    public int GetNumber()
    {
        return _number;
    }

    private void OnClick()
    {
        if (_readyToMove)
        {
            Vector3 newMovePosition = ServiceLocator.SpotsGame.GetMovePosition(_rectTransform.localPosition);

            if (newMovePosition != Vector3.zero)
            {
                _previousPosition = _rectTransform.localPosition;
                _timer = 0;
                StartCoroutine(MoveTo(newMovePosition));
            }
        }
    }

    private IEnumerator MoveTo(Vector3 newPosition)
    {
        _readyToMove = false;
        
        float speed = (_rectTransform.localPosition - newPosition).magnitude / (_moveTime * _smooth);
        
        while (_timer < _moveTime)
        {
            _timer += Time.deltaTime;
            
            _rectTransform.localPosition = Vector3.Lerp(_rectTransform.localPosition, newPosition, Time.deltaTime * speed);
            
            yield return null;
        }
        
        SetPosition(newPosition);
        OnMoveEnd?.Invoke(_previousPosition, newPosition, this);
        _readyToMove = true;
    }
}