using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairsGame : MonoBehaviour
{
    [SerializeField] private Transform _pairsBox;
    [SerializeField] private PairsCard _pairsItemPrefab;
    [SerializeField] private int _secondsToLoose = 60;
    [SerializeField] private int _timeToCardClose = 1;
    [SerializeField] private List<Sprite> _cardsSprites = new List<Sprite>(); 

    private bool _isGameRunning = false;
    private bool _isCanOpenCard = true;
    private float _timer = 1000;
    private List<Card> _cards = new List<Card>();
    private List<PairsCard> _pairs = new List<PairsCard>();
    
    private int _horizontalCount = 5;
    private int _verticalCount = 2;
    private int _xStartPosition = -600;
    private int _yStartPosition = 90;
    private int _xCurrentPosition;
    private int _yCurrentPosition;
    
    public event Action<float, bool> OnTimerChange;
    
    public bool IsGameRunning => _isGameRunning;
    
    private void Awake()
    {
        ServiceLocator.PairsGame = this;

        EventBus.MiniGamesEvents.OnPairsGameStart += OnGameStart;
        EventBus.MiniGamesEvents.OnPairsGameEnd += OnGameEnd;

        for (int i = 0; i < _cardsSprites.Count; i++)
        {
            _cards.Add(new Card(i, _cardsSprites[i]));
        }
    }

    private void OnDestroy()
    {
        EventBus.MiniGamesEvents.OnPairsGameStart -= OnGameStart;
        EventBus.MiniGamesEvents.OnPairsGameEnd -= OnGameEnd;
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            _timer -= Time.deltaTime;
            
            OnTimerChange?.Invoke(_timer, _timer / _secondsToLoose < 0.3f);
            
            if (_timer <= 0)
            {
                EventBus.MiniGamesEvents.OnPairsGameEnd?.Invoke(false);
            }
        }
    }

    private void OnGameStart()
    {
        GenerateCards();
        _isGameRunning = true;
        _timer = _secondsToLoose;
    }
    
    private void OnGameEnd(bool success)
    {
        _isGameRunning = false;
        DestroyItems();
    }

    private void GenerateCards()
    {
        List<Card> ShuffleIntList(List<Card> list)
        {
            var random = new System.Random();
            var tmpList = new List<Card>();

            foreach (var card in list)
            {
                tmpList.Add(card);
            }
            
            var newShuffledList = new List<Card>();
            var listCount = tmpList.Count;
            
            for (int i = 0; i < listCount; i++)
            {
                var randomElementInList = random.Next(0, tmpList.Count);
                newShuffledList.Add(tmpList[randomElementInList]);
                tmpList.Remove(tmpList[randomElementInList]);
            }
            
            return newShuffledList;
        }
        
        List<Card> GetRandomCard(List<Card> cards)
        {
            var resultList = new List<Card>();
            var shuffledCards = ShuffleIntList(cards);

            for (int i = 0; i < _horizontalCount; i++)
            {
                resultList.Add(shuffledCards[i]);
                resultList.Add(shuffledCards[i]);
            }
            
            resultList = ShuffleIntList(resultList);

            return resultList;
        }
        
        DestroyItems();

        var cards = GetRandomCard(_cards);
        
        int count = 0;
        
        for (int i = 0; i < _horizontalCount; i++)
        {
            for (int j = 0; j < _verticalCount; j++)
            {
                PairsCard pairsCard = Instantiate(_pairsItemPrefab, _pairsBox);
                pairsCard.SetCard(count, cards[count].Number, cards[count].CardSprite, _xCurrentPosition, _yCurrentPosition);
                pairsCard.OnCardOpened += OnCardOpened;
                    
                _pairs.Add(pairsCard);

                _yCurrentPosition -= 400;

                count++;
            }

            _xCurrentPosition += 300;
            _yCurrentPosition = _yStartPosition;
        }
    }
    
    private void DestroyItems()
    {
        foreach (var pair in _pairs)
        {
            pair.OnCardOpened -= OnCardOpened;
        }

        for (int i = 0; i < _pairsBox.childCount; i++)
        {
            Destroy(_pairsBox.GetChild(i).gameObject);
        }

        _pairs.Clear();
        _xCurrentPosition = _xStartPosition;
        _yCurrentPosition = _yStartPosition;
    }

    private void OnCardOpened(PairsCard card)
    {
        if (_isCanOpenCard && _isGameRunning)
        {
            card.OpenCard();
            
            List<PairsCard> openedCards = new List<PairsCard>();

            foreach (var pair in _pairs)
            {
                if (pair.IsDone == false && pair.IsOpened)
                {
                    openedCards.Add(pair);
                }
            }

            if (openedCards.Count == 2)
            {
                if (openedCards[0].Number != openedCards[1].Number)
                {
                    StartCoroutine(CloseCard(openedCards));
                }
                else
                {
                    openedCards[0].SetDone();
                    openedCards[1].SetDone();
                }
            }
        
            CheckGameWin();
        }
    }

    private IEnumerator CloseCard(List<PairsCard> openedCards)
    {
        _isCanOpenCard = false;
        
        yield return new WaitForSeconds(_timeToCardClose);

        if (_isGameRunning)
        {
            openedCards[0].CloseCard();
            openedCards[1].CloseCard();
        }

        _isCanOpenCard = true;
    }
    
    private void CheckGameWin()
    {
        bool isGameWin = true;
        
        foreach (var pair in _pairs)
        {
            if (pair.IsDone == false)
                isGameWin = false;
        }
        
        if(isGameWin)
        {
            _isGameRunning = false;
            EventBus.MiniGamesEvents.OnPairsGameEnd?.Invoke(true);
        }
    }
}