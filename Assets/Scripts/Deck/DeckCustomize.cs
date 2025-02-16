using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class DeckCustomize : MonoBehaviour
{
    [SerializeField] GameObject deckContents;
    [SerializeField] GameObject cardContents;

    public Deck deck;
    
    public void SerCardToCustom(Card card)
    {
        card.OnClickCard = SelectedDeckCard;
    }

    //カードクリック時のリアクション
    public void SelectedDeckCard(Card card)
    {
        if (card.transform.parent == deckContents.transform)
        {
            int dest = deck.DeckAll.FindIndex(number => number == card.Base.Number);
            
            deck.DeckAll.RemoveAt(dest);
            Destroy(deck.LookDeck[dest].gameObject);
            deck.LookDeck.RemoveAt(dest);

            deck.deckArignment();
            card.PosReset();
        }
        else if (deck.DeckAll.Count < 15 && card.transform.parent == cardContents.transform)
        {
            deck.DeckAll.Add(card.Base.Number);
            Card newCard = deck.Generator.Spawn(card.Base.Number);
            SerCardToCustom(newCard);
            newCard.transform.SetParent(deckContents.transform);
            deck.LookDeck.Add(newCard);

            deck.deckArignment();
        }
        else if (deck.DeckAll.Count == 15 )
        {

        }
    }
}
