using UnityEngine;

public class Card
{
    private int _number;
    private Sprite _cardSprite;
    
    public int Number => _number;
    public Sprite CardSprite => _cardSprite;

    public Card(int number, Sprite cardSprite)
    {
        _number = number;
        _cardSprite = cardSprite;
    }
}