using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Game game;

    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite findSprite;

    [SerializeField] private int number;

    public bool Down => this.GetComponent<Image>().sprite == frontSprite;
    public bool Find => this.GetComponent<Image>().sprite == findSprite;
    public int Number => number;

    public void OnMouseDown()
    {
        if (!Find)
        {
            if (!game.FindCard)
            {
                if (!Down)
                {
                    this.GetComponent<Image>().sprite = frontSprite;
                    game.OpenCards();
                }
            }
        }
    }
    public void NoFindCard()
    {
        this.GetComponent<Image>().sprite = backSprite;
    }
    public void FindCard()
    {
        this.GetComponent<Image>().sprite = findSprite;
    }
}
