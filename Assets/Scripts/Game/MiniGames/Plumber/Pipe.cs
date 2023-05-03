using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Sprite _spriteWithWater;
    [SerializeField] private Sprite _spriteWithoutWater;
    [SerializeField] private bool _needToRotate = true;
    [SerializeField] private List<int> _correctRotation = new List<int>();
    
    float[] _rotations = { 0, 90, 180, 270 };
    
    private Button _button;
    private Image _image;
    private bool _isHaveWater;
    private float _startingRotation;
    
    public bool IsHaveWater => _isHaveWater;

    public bool CheckRotation()
    {
        return _correctRotation.Contains((int)transform.eulerAngles.z);
    }

    public void SetWater(bool isHaveWater)
    {
        _isHaveWater = isHaveWater;
        _image.sprite = isHaveWater ? _spriteWithWater : _spriteWithoutWater;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _button.onClick.AddListener(OnClick);

        _startingRotation = transform.eulerAngles.z;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnEnable()
    {
        _isHaveWater = false;
        _image.sprite = _spriteWithoutWater;

        if (_needToRotate)
        {
            int random = UnityEngine.Random.Range(0, _rotations.Length);
            transform.eulerAngles = new Vector3(0, 0, _rotations[random]); 
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, _startingRotation);
        }
    }
    
    private void OnClick()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        ServiceLocator.PlumberGame.CheckGameWin();
    }
}