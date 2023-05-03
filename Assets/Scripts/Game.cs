using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform panelCard;
    [SerializeField] private List<Card> cards;

    [SerializeField] private int numberCard;
    [SerializeField] private bool findCard;

    [SerializeField] private GameObject winPanel;

    public bool FindCard => findCard;

    public void Awake()
    {
        for (int i = 0; i < panelCard.childCount; i++)
        {
            panelCard.GetChild(i).transform.SetSiblingIndex(Random.Range(0, panelCard.childCount));
        }
    }

    public void Start()
    {
        for (int i = 0; i < panelCard.childCount; i++)
        {
            cards.Add(panelCard.GetChild(i).GetComponent<Card>());
        }
    }

    public void OpenCards()
    {
        var openCard = 0;

        foreach (Card card in cards)
        {
            if (card.Down)
            {
                if (openCard == 0)
                {
                    numberCard = card.Number;
                }
                openCard++;
            }
        }

        if (openCard == 2)
        {
            CheckCard();
        }
    }

    public void CheckCard()
    {
        var a = 0;

        foreach (Card card in cards)
        {
            if (card.Down && card.Number == numberCard)
            {
                a++;
            }
        }

        if (a == 2)
        {
            FindCards();
        }
        else
        {
            NoFindCards();
        }
    }

    public void FindCards()
    {
        foreach (Card card in cards)
        {
            if (card.Down)
            {
                StartCoroutine(Win(card));
            }
        }
    }

    public void NoFindCards()
    {
        foreach (Card card in cards)
        {
            if (card.Down)
            {
                StartCoroutine(NoFind(card));
            }
        }
    }

    IEnumerator NoFind(Card card)
    {
        findCard = true;
        yield return new WaitForSeconds(1f);
        card.GetComponent<Card>().NoFindCard();
        findCard = false;
    }

    IEnumerator Win(Card card)
    {
        findCard = true;
        yield return new WaitForSeconds(1f);
        card.GetComponent<Card>().FindCard();
        findCard = false;
        Win();
    }

    public void Win()
    {
        var a = 0;

        foreach (Card card in cards)
        {
            if (card.Find)
            {
                a++;
            }

        }

        if (a == panelCard.childCount)
        {
            winPanel.SetActive(true);
        }
    }
}
