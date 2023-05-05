using System;
using System.Collections.Generic;
using UnityEngine;

public class PlumberGame : MonoBehaviour
{
    [SerializeField] private Transform _plumberBox;
    [SerializeField] private SpotItem _pipeItemPrefab;
    [SerializeField] private RectTransform _backgroundItemPrefab;
    [SerializeField] private int _secondsToLoose = 30;

    [SerializeField] private int _gridSizeX = 6;
    [SerializeField] private int _gridSizeY = 6;
    [SerializeField] private List<Pipe> _pipes = new List<Pipe>();

    private int _xStartPosition = -300;
    private int _yStartPosition = 275;
    private int _xCurrentPosition;
    private int _yCurrentPosition;
    private int _step = 100;
    
    private bool _isGameRunning = false;
    private float _timer = 1000;
    
    public event Action<float, bool> OnTimerChange;
    
    public bool IsGameRunning => _isGameRunning;

    public void CheckGameWin()
    {
        int correctPipes = 0;
        
        for (int i = 0; i < _pipes.Count; i++)
        {
            if (_pipes[i].CheckRotation())
            {
                if (i == 0)
                {
                    correctPipes++;
                    _pipes[i].SetWater(true);
                }
                else if(_pipes[i - 1].IsHaveWater)
                {
                    correctPipes++;
                    _pipes[i].SetWater(true);
                }
            }
            else
            {
                _pipes[i].SetWater(false);
            }
        }

        if (correctPipes == _pipes.Count)
        {
            _isGameRunning = false;
            EventBus.MiniGamesEvents.OnPlumberGameEnd?.Invoke(true);
        }
    }
    
    [ContextMenu("Create Grid")]
    public void CreateGrid() {
        
        _xCurrentPosition = _xStartPosition;
        _yCurrentPosition = _yStartPosition;
        
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                CreateGridCell(_xCurrentPosition, _yCurrentPosition);
                
                _yCurrentPosition -= _step;
            }
            
            _xCurrentPosition += _step;
            _yCurrentPosition = _yStartPosition;
        }
    }

    private void CreateGridCell(int x, int y)
    {
        var gridCell = Instantiate(_backgroundItemPrefab, new Vector3(x, y, 0), Quaternion.identity, _plumberBox);
        gridCell.name = $"background ({x},{y})";
        gridCell.localPosition = new Vector2(x, y);
    }
    
    private void Awake()
    {
        ServiceLocator.PlumberGame = this;

        EventBus.MiniGamesEvents.OnPlumberGameStart += OnGameStart;
        EventBus.MiniGamesEvents.OnPlumberGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        EventBus.MiniGamesEvents.OnPlumberGameStart -= OnGameStart;
        EventBus.MiniGamesEvents.OnPlumberGameEnd -= OnGameEnd;
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            _timer -= Time.deltaTime;
            
            OnTimerChange?.Invoke(_timer, _timer / _secondsToLoose < 0.3f);
            
            if (_timer <= 0)
            {
                EventBus.MiniGamesEvents.OnPlumberGameEnd?.Invoke(false);
            }
        }
    }
    
    private void OnGameStart()
    {
        _isGameRunning = true;
        _timer = _secondsToLoose;
        _plumberBox.gameObject.SetActive(true);
        
        CheckGameWin();
    }

    private void OnGameEnd(bool success)
    {
        _isGameRunning = false;
        _plumberBox.gameObject.SetActive(false);
    }
}