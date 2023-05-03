using System;
using UnityEngine;
using UnityEngine.UI;

public class PairsCard : MonoBehaviour
{
    [SerializeField] private Sprite _closeCard;
    
    private RectTransform _rectTransform;
    private Button _button;
    private Image _image;
    private Sprite _openCard;

    private int _index;
    private int _number;
    private bool _isOpened = false;
    private bool _isDone = false;
    
    public event Action<PairsCard> OnCardOpened;

    public int Index => _index;
    public int Number => _number;
    public bool IsOpened => _isOpened;
    public bool IsDone => _isDone;
    
    public void SetCard(int index, int number, Sprite card, int x, int y)
    {
        _index = index;
        _number = number;
        _openCard = card;
        _rectTransform.localPosition = new Vector3(x, y, 0);
    }
    
    public void OpenCard()
    {
        _isOpened = true;
        _image.sprite = _openCard;
    }
    
    public void CloseCard()
    {
        _isOpened = false;
        _image.sprite = _closeCard;
    }
    
    public void SetDone()
    {
        _isDone = true;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _button.onClick.AddListener(() =>
        {
            if(_isOpened == false && _isDone == false)
                OnCardOpened?.Invoke(this);
        });
    }
}