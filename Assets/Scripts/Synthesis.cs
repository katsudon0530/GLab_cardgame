using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Synthesis : MonoBehaviour
{
    [SerializeField] GameMaster master;
    [SerializeField] CardGenerator generator;
    [SerializeField] BattlerHand hand;
    [SerializeField] Battler battler;
    [SerializeField] Text kekka;
    [SerializeField] GameObject SynthesisButtonPanel;
    public Card SynthesisCard;
    public int cardNumber = 0;


    //カードを合成する
    public void CardSynthesis(List<Card> card, List<int> Deck)
    {
        switch (card[0].Base.Type)
        {
            case CardType.Sword:
                cardNumber = 4;
                break;
            case CardType.Witchcraft:
                cardNumber = 5;
                break;
            case CardType.Protection:
                cardNumber = 6;
                break;
            case CardType.Heal:
                cardNumber = 7;
                break;
        }
        //合成したカードを手札に生成
        Deck.Add(cardNumber);
        SynthesisCard = generator.Spawn(cardNumber);
        battler.SerCardToHand(SynthesisCard);
        battler.Hand.ResetPosition();

        //デッキから合成したカードを削除
        for (int i = 0; i < card.Count; i++)
        {
            int index = Deck.FindIndex(number => number == card[i].Base.Number);
            Deck.RemoveAt(index);
        }

        //合成した場のカードを壊す
        for (int i = 1; card.Count > 1;)
        {
            Destroy(card[i].gameObject);
            card.RemoveAt(i);
        }

    }


    //カードを移動させて回転
    public IEnumerator Close(Card card)
    {
        float rotationAngle = 180.0f;
        float duration = 0.5f;

        float elapsedTime = 0.0f;
        Quaternion startRotation = card.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, rotationAngle, 0);

        //一回転目
        while (elapsedTime < duration)
        {
            // 経過時間に基づいて回転を線形補間
            card.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            if (200.0f < card.transform.eulerAngles.y && card.transform.eulerAngles.y < 270.0f)
            {
                card.Close();
            }
            yield return null; 
        }

        // 最後に目標の回転にぴったり合わせる
        card.transform.rotation = endRotation;
        kekka.text = "カードを合成した";
        yield return new WaitForSeconds(0.8f);

        rotationAngle = 0.0f;
        elapsedTime = 0.0f;
        startRotation = card.transform.rotation;
        endRotation = Quaternion.Euler(0, rotationAngle, 0);
        //2回転目
        while (elapsedTime < duration)
        {
            // 経過時間に基づいて回転を線形補間
            card.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            if (270f < card.transform.eulerAngles.y )
            {
                card.Open();
                generator.ChangeCard(card,cardNumber);
            }
            yield return null;  // 次のフレームまで待機
        }

        // 最後に目標の回転にぴったり合わせる
        card.transform.rotation = endRotation;
    }


    //裏面を表示
    public void OnSynthesisPanel()
    {
        SynthesisButtonPanel.SetActive(true);
    }

    //裏面を非表示
    public void OffSynthesisPanel()
    {
        SynthesisButtonPanel.SetActive(false);
    }
}
