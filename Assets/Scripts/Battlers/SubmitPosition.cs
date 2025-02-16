using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPosition : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] Synthesis synthesis;
    Card submitCard;

    public List<Card> submitlist = new();
    public Card SubmitCard { get => submitCard; set => submitCard = value; }
    public List<Card> Submitlist { get => submitlist; set => submitlist = value; }


    //カードをフィールドにセットする
    public void Set(Card card)
    {
        if (Submitlist.Count < 2 )
        {
            Add(card);
            if (Submitlist.Count > 1)
            {
                synthesis.OffSynthesisPanel();
            }
        }
        else if (Submitlist.Count == 2 )
        {
            Add(card);
            SubmitCard = card;
        }
        else
        {
            return;
        }
        effectSwitch(Submitlist);
    }

    //場のカードを消去する
    public void DeleteCard()
    {
        for (int i = 0; i < Submitlist.Count; i++)
        {
            Destroy(Submitlist[i].gameObject);
        }
        Submitlist.Clear();
        submitCard = null;
    }

    //カードをリストに追加
    void Add(Card card)
    {
        Submitlist.Add(card);
        card.transform.SetParent(this.transform);
        card.transform.position = transform.position;
        SubmitPositionIn();
    }

    //カードをリストから削除
    public void ReRemove(Card card)
    {
        Submitlist.Remove(card);
        if (Submitlist.Count < 2)
        {
            synthesis.OnSynthesisPanel();
        }
    }

    //フィールドに置かれたカードの位置を整列させる
    public void SubmitPositionIn()
    {
        for (int i = 0; i < Submitlist.Count; i++)
        {
            float posX = 2.5f * (i - 1f);
            Submitlist[i].transform.localPosition = new Vector3(posX, 0);

            Transform childTransform = Submitlist[i].transform.GetChild(0);
            Canvas canvas = childTransform.GetComponent<Canvas>();
            canvas.sortingOrder = 2 - i;
        }
        effectSwitch(Submitlist);
    }

    //どのエフェクトが付くかを判別する
    public void effectSwitch(List<Card> submitCards)
    {
        for(int i = 0;i < submitCards.Count;i++)
        {
            
            effectReSet(submitCards[i]);
            if (i == 0) { }
            else if (submitCards[i].Base.Type == CardType.Sword)
                effectJudgement(submitCards[i - 1].Base.CardEffect.Attack_Effect, submitCards[i]);

            else if (submitCards[i].Base.Type == CardType.Witchcraft)
                effectJudgement(submitCards[i - 1].Base.CardEffect.Magic_Effect, submitCards[i]);

            else if (submitCards[i].Base.Type == CardType.Protection)
                effectJudgement(submitCards[i - 1].Base.CardEffect.Protection_Effect, submitCards[i]);

            else if (submitCards[i].Base.Type == CardType.Heal)
                effectJudgement(submitCards[i - 1].Base.CardEffect.Heal_Effect , submitCards[i]);
        }
    }

    //エフェクトを表示する
    private void effectJudgement(float magnification,Card card)
    {
        effectScaleReSet(card);
        if (magnification < 1f)
        {
            card.EffectDown.gameObject.transform.localScale *= (2f - magnification);
            card.EffectDown.gameObject.SetActive(true);
        }
        else if(magnification > 1f)
        {
            card.EffectUp.gameObject.transform.localScale *= (1f + 0.3f * (magnification - 1f));
            card.EffectUp.gameObject.SetActive(true);
        }
        else
        {
            effectReSet(card);
        }
    }

    //エフェクト表示を消す
    public void effectReSet(Card card)
    {
        effectScaleReSet(card);
        card.EffectUp.SetActive(false);
        card.EffectDown.SetActive(false);
    }

    void effectScaleReSet(Card card)
    {
        card.EffectDown.gameObject.transform.localScale = Vector3.one;
        card.EffectUp.gameObject.transform.localScale = Vector3.one;
    }
}
