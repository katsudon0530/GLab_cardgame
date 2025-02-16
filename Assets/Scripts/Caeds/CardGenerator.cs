using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{

    [SerializeField] Card cardPrefab;
    [SerializeField] CardBase[] cardBases;

    public CardBase[] CardBases { get => cardBases; set => cardBases = value; }

    //ナンバーからカードを生成する
    public Card Spawn(int number)
    {
        Card card = Instantiate(cardPrefab);
        card.Set(CardBases[number]);
        return card;
    }

    //カードの情報を更新する
    public Card ChangeCard(Card card, int number)
    {
        card.Set(CardBases[number]);
        return card;
    }

}
