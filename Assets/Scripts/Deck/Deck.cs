using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] CardGenerator generator;
    [SerializeField] GameObject DeckContents;
    [SerializeField] GameObject CardContents;
    [SerializeField] GameObject Contents;
    [SerializeField] GameObject Contents2;
    [SerializeField] GameObject ScrollScale;
    [SerializeField] Text RestText;
    [SerializeField] DeckCustomize customize;

    [SerializeField] GameObject DeckPanel;
    [SerializeField] GameObject CardList;

    [SerializeField] int DeckWidth;
    [SerializeField] int DeckHeight;
    [SerializeField] float cardInterval;

    public List<int> Base;
    public List<int> DeckAll;
    public List<int> cardDeck;
    public List<Card> LookDeck;
    public List<Card> LookCards;

    public CardGenerator Generator { get => generator; set => generator = value; }

    //ゲーム開始時にデッキをデフォルト状態にする
    public void DeckDefault()
    {
        DeckAll = new List<int>(Base);
        DeckListOpen();
        CustomCardListOpen();
    }

    //カスタマイズしたデッキをセットする
    public void DeckSet()
    {
        cardDeck = new List<int>(DeckAll);
        DeckPanel.SetActive(true);
        CardList.SetActive(false);
    }

    //デッキリストの一覧を表示する
    public void DeckListOpen()
    {

        for (int i = DeckContents.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(DeckContents.transform.GetChild(i).gameObject);
        }
        LookDeck.Clear();

        for (int i = 0; i < DeckAll.Count; i++)
        {
            Card card = Generator.Spawn(DeckAll[i]);
            LookDeck.Add(card);
            card.transform.SetParent(DeckContents.transform);
            customize.SerCardToCustom(card);

        }

        deckArignment();
    }

    //カスタマイズするためのカードを表示する
    public void CustomCardListOpen()
    {
        for (int i = 0; i <= (Generator.CardBases.Length - 1); i++)
        {
            if (Generator.CardBases[i].SynthesisType == SynthesisType.Normal)
            {
                Card card = Generator.Spawn(i);
                LookCards.Add(card);
                card.transform.SetParent(CardContents.transform);
                customize.SerCardToCustom(card);
            }
        }
        CustomCardArignment();
    }

    //デッキをソートして配置する
    public void deckArignment()
    {
        Vector2 currentScale;
        int posX;
        float posY;
        int DeckLookCount = DeckWidth;

        int a = (LookDeck.Count + DeckWidth - 1) / DeckWidth;
        LookDeck.Sort((card0, card1) => card0.Base.Number - card1.Base.Number);

        //スクロール縦幅の設定
        currentScale = Contents.GetComponent<RectTransform>().sizeDelta;
        currentScale.y = DeckHeight * a + 50;
        Contents.GetComponent<RectTransform>().sizeDelta = currentScale;

        //横の幅を設定する
        currentScale = ScrollScale.GetComponent<RectTransform>().sizeDelta;
        currentScale.x = 240f * DeckLookCount;
        ScrollScale.GetComponent<RectTransform>().sizeDelta = currentScale;
        ScrollScale.transform.localPosition = new Vector3(currentScale.x / 2 - 825, -25);

        for (int i = 0; i < a; i++)
        {
            if (a % 2 == 0)
            {
                posY = (float)((i - a / 2) * -DeckHeight - DeckHeight / 2);
            }
            else
            {
                posY = (float)((i - a / 2) * -DeckHeight);
            }

            if (LookDeck.Count <= ((i + 1) * DeckWidth))
            {
                DeckLookCount = LookDeck.Count - i * DeckWidth;
                ;
            }
            for (int j = 0; j < DeckLookCount; j++)
            {
                if (DeckWidth % 2 == 0)
                {
                    posX = (int)((j - DeckWidth / 2) * cardInterval + cardInterval / 2);
                }
                else
                {
                    posX = (int)((j - DeckWidth / 2) * cardInterval);
                }
                LookDeck[j + i * DeckWidth].transform.localPosition = new Vector3(posX, posY);
            }
        }
    }

    //カスタマイズカードを並べる
    public void CustomCardArignment()
    {
        Vector2 currentScale;
        float posY;

        LookCards.Sort((card0, card1) => card0.Base.Number - card1.Base.Number);

        //スクロール縦幅の設定
        currentScale = Contents2.GetComponent<RectTransform>().sizeDelta;
        currentScale.y = DeckHeight * LookCards.Count + 50;
        Contents2.GetComponent<RectTransform>().sizeDelta = currentScale;

        for (int i = 0; i < LookCards.Count; i++)
        {
            if (LookCards.Count % 2 == 0)
            {
                posY = (float)((i - LookCards.Count / 2) * -DeckHeight - DeckHeight / 2);
            }
            else
            {
                posY = (float)((i - LookCards.Count / 2) * -DeckHeight);
            }


            LookCards[i].transform.localPosition = new Vector3(0, posY);

        }
    }

    //デッキの残り枚数を表示する
    public void RestDeck()
    {
        RestText.text = $"残り{cardDeck.Count}枚";
    }
}
