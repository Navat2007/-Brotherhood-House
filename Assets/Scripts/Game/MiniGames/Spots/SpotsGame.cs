using System;
using System.Collections.Generic;
using UnityEngine;

public class SpotsGame : MonoBehaviour
{
    [SerializeField] private Transform _spotsBox;
    [SerializeField] private SpotItem _spotItemPrefab;
    [SerializeField] private int _secondsToLoose = 60;

    private Dictionary<Vector2, SpotItem> _spots = new Dictionary<Vector2, SpotItem>();

    private int _xStartPosition = -300;
    private int _yStartPosition = 200;
    private int _minXPosition = -300;
    private int _maxXPosition = 300;
    private int _minYPosition = -400;
    private int _maxYPosition = 200;

    private int _horizontalCount = 4;
    private int _verticalCount = 4;
    private int _xCurrentPosition;
    private int _yCurrentPosition;
    
    private bool _isGameRunning = false;
    private float _timer = 1000;
    
    public event Action<float, bool> OnTimerChange;

    public Vector3 GetMovePosition(Vector2 position)
    {
        if ((int)position.x < _maxXPosition && _spots.ContainsKey(new Vector2(position.x + 200, position.y)) == false)
        {
            return new Vector2(position.x + 200, position.y);
        }
        
        if ((int)position.x > _minXPosition && _spots.ContainsKey(new Vector2(position.x - 200, position.y)) == false)
        {
            return new Vector2(position.x - 200, position.y);
        }
        
        if ((int)position.y < _maxYPosition && _spots.ContainsKey(new Vector2(position.x, position.y + 200)) == false)
        {
            return new Vector2(position.x, position.y + 200);
        }
        
        if ((int)position.y > _minYPosition && _spots.ContainsKey(new Vector2(position.x, position.y - 200)) == false)
        {
            return new Vector2(position.x, position.y - 200);
        }

        return Vector3.zero;
    }

    private void Awake()
    {
        ServiceLocator.SpotsGame = this;

        EventBus.MiniGamesEvents.OnSpotsGameStart += OnGameStart;
        EventBus.MiniGamesEvents.OnSpotsGameEnd += OnGameEnd;
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            _timer -= Time.deltaTime;
            
            OnTimerChange?.Invoke(_timer, _timer / _secondsToLoose < 0.3f);
            
            if (_timer <= 0)
            {
                EventBus.MiniGamesEvents.OnSpotsGameEnd?.Invoke(false);
            }
        }
    }

    private void OnGameStart()
    {
        GenerateTable();
        _isGameRunning = true;
        _timer = _secondsToLoose;
    }
    
    private void OnGameEnd(bool obj)
    {
        _isGameRunning = false;
        DestroyItems();
    }

    private void DestroyItems()
    {
        foreach (var spot in _spots)
        {
            spot.Value.OnMoveEnd -= OnSpotMoveEnd;
        }

        for (int i = 0; i < _spotsBox.childCount; i++)
        {
            Destroy(_spotsBox.GetChild(i).gameObject);
        }

        _spots.Clear();
        _xCurrentPosition = _xStartPosition;
        _yCurrentPosition = _yStartPosition;
    }

    private void GenerateTable()
    {
        int GetRandomNumber(ref List<int> numbers)
        {
            int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
            int result = numbers[randomIndex];

            numbers.RemoveAt(randomIndex);

            return result;
        }

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        DestroyItems();

        for (int i = 0; i < _horizontalCount; i++)
        {
            for (int j = 0; j < _verticalCount; j++)
            {
                if (numbers.Count > 0)
                {
                    SpotItem spotItem = Instantiate(_spotItemPrefab, _spotsBox);
                    spotItem.SetNumber(GetRandomNumber(ref numbers));
                    spotItem.SetPosition(new Vector3(_xCurrentPosition, _yCurrentPosition));
                    spotItem.OnMoveEnd += OnSpotMoveEnd;
                    
                    _spots.Add(new Vector2(_xCurrentPosition, _yCurrentPosition), spotItem);

                    _yCurrentPosition -= 200;
                }
            }

            _xCurrentPosition += 200;
            _yCurrentPosition = _yStartPosition;
        }
    }

    private void OnSpotMoveEnd(Vector2 previousPosition , Vector2 currentPosition, SpotItem spotItem)
    {
        _spots.Add(currentPosition, spotItem);
        _spots.Remove(previousPosition);
        
        CheckGameWin();
    }

    private void CheckGameWin()
    {
        bool isGameWin = true;
        
        foreach (var spot in _spots)
        {
            switch (spot.Value.GetNumber())
            {
                case 1:
                    if(spot.Key != new Vector2(-300, 200))
                        isGameWin = false;
                    break;
                
                case 2:
                    if(spot.Key != new Vector2(-100, 200))
                        isGameWin = false;
                    break;
                
                case 3:
                    if(spot.Key != new Vector2(100, 200))
                        isGameWin = false;
                    break;
                
                case 4:
                    if(spot.Key != new Vector2(300, 200))
                        isGameWin = false;
                    break;
                
                case 5:
                    if(spot.Key != new Vector2(-300, 0))
                        isGameWin = false;
                    break;
                
                case 6:
                    if(spot.Key != new Vector2(-100, 0))
                        isGameWin = false;
                    break;
                
                case 7:
                    if(spot.Key != new Vector2(100, 0))
                        isGameWin = false;
                    break;
                
                case 8:
                    if(spot.Key != new Vector2(300, 0))
                        isGameWin = false;
                    break;
                
                case 9:
                    if(spot.Key != new Vector2(-300, -200))
                        isGameWin = false;
                    break;
                
                case 10:
                    if(spot.Key != new Vector2(-100, -200))
                        isGameWin = false;
                    break;
                
                case 11:
                    if(spot.Key != new Vector2(100, -200))
                        isGameWin = false;
                    break;
                
                case 12:
                    if(spot.Key != new Vector2(300, -200))
                        isGameWin = false;
                    break;
                
                case 13:
                    if(spot.Key != new Vector2(-300, -400))
                        isGameWin = false;
                    break;
                
                case 14:
                    if(spot.Key != new Vector2(-100, -400))
                        isGameWin = false;
                    break;
                
                case 15:
                    if(spot.Key != new Vector2(100, -400))
                        isGameWin = false;
                    break;
            }
        }
        
        if(isGameWin)
        {
            _isGameRunning = false;
            EventBus.MiniGamesEvents.OnSpotsGameEnd?.Invoke(true);
        }
    }
}