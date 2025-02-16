using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{

    [SerializeField] Card cardPrefab;
    [SerializeField] CardBase[] cardBases;

    public CardBase[] CardBases { get => cardBases; set => cardBases = value; }

    //�i���o�[����J�[�h�𐶐�����
    public Card Spawn(int number)
    {
        Card card = Instantiate(cardPrefab);
        card.Set(CardBases[number]);
        return card;
    }

    //�J�[�h�̏����X�V����
    public Card ChangeCard(Card card, int number)
    {
        card.Set(CardBases[number]);
        return card;
    }

}
