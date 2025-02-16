using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] EnemyGenerator enemyGenerator;
    [SerializeField] EnemyFiled enemyFiled;
    [SerializeField] Deck deck;
    [SerializeField] GameObject submitButton;
    [SerializeField] GameObject SynthesisButton;
    [SerializeField] GameObject serectPanel;
    [SerializeField] GameObject cardGuide;
    [SerializeField] RuleBook ruleBook;
    [SerializeField] GameUI gameUI;
    [SerializeField] Synthesis synthesis;
    [SerializeField] int handMax;

    public int enemyNum;
    int cardsum;
    Enemy enemy;

    int TurnCount;


    private void Start()
    {
        gameUI.UISetUp();
        deck.DeckDefault();
    }

    //モンスターをセレクトした後のセットアップ
    public void Serect()
    {
        serectPanel.SetActive(false);
        Setup();
    }
    //ゲームスタート時のセットアップ内容
    public void Setup()
    {
        player.SetPlayer();
        gameUI.ShowLifes(player.Life);
        ruleBook.TextSetupNext();
        player.OnSubmitAction = SubmittedAction;
        player.OnSynthesisAction = SynthesisAction;
        float handWidth = (float)(player.Hand.cardInterval * 6);
        player.Hand.cardInterval = (float)(handWidth / handMax);
        SendCardTo(player);
        enemy = enemyGenerator.SpawnEnemy(enemyNum);
        enemyFiled.AddEnemy(enemy);
        synthesis.OnSynthesisPanel();
        deck.DeckListOpen();
        deck.DeckSet();
        TurnSetup();
    }

    void TurnSetup()
    {
        TurnCount = 1;
        gameUI.ShowTurn(TurnCount);
    }

    //フィールドにカードが設置されているかの判定・決定ボタンの削除

    void SubmittedAction()
    {
        submitButton.SetActive(false);
        SynthesisButton.SetActive(false);

        StartCoroutine(CardBattle());

    }

    //フィールドにカードが設置されているかの判定・決定ボタンの削除
    void SynthesisAction()
    {
        submitButton.SetActive(false);
        SynthesisButton.SetActive(false);
        StartCoroutine(CardSynthesis());

    }

    //手札を生成
    void SendCardTo(Battler battler)
    {
        if(deck.cardDeck.Count != 0)
        {
            cardsum = handMax - battler.Hand.List.Count;
            if (cardsum > deck.cardDeck.Count)
            {
                cardsum = deck.cardDeck.Count;
            }
        }
        else if (deck.cardDeck.Count == 0 && battler.Hand.List.Count == 0)
        {
            deck.cardDeck = new List<int>(deck.DeckAll);
            cardsum = handMax;
            if (cardsum > deck.cardDeck.Count)
            {
                cardsum = deck.cardDeck.Count;
            }
        }
        else if(deck.cardDeck.Count == 0)
        {
            cardsum = 0;
        }


        for (int i = 0; i < cardsum; i++)
        {
            int r = Random.Range(0, deck.cardDeck.Count);
            Card card = cardGenerator.Spawn(deck.cardDeck[r]);
            deck.cardDeck.RemoveAt(r);
            battler.SubmitPosition.effectReSet(card);
            battler.SerCardToHand(card);
        }
        battler.Hand.ResetPosition();
        deck.RestDeck();
    }

    //カードバトル・勝敗判定
    IEnumerator CardBattle()
    {
        gameUI.KekkaPanel.SetActive(true);
        player.Hand.gameObject.SetActive(false);
        cardGuide.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        for (int i = 0;i < player.SubmitList.Count; i++)
        {
            Card card = player.SubmitList[i];
            Card flontCard = null;
            if (i != 0)
            {
                flontCard = player.SubmitList[i - 1];
            }
            card.transform.position += Vector3.up * 0.2f;
            ruleBook.FlontEffect(player, flontCard);
            ruleBook.TypeEffect(player, card);
            ruleBook.selectedCardVS(player, card, enemy);
            gameUI.ShowLifes(player.Life);
            enemy.EnemyLifeContlloer.lifeReflection(enemy);
            yield return new WaitForSeconds(1.2f);
            if (enemy.Base.EnemyLife == 0)
            {
                ShowResult();
                yield break;
            }
            player.SetStatus();
        }

        yield return new WaitForSeconds(1f);
        ruleBook.EnemyParLife(enemy);
        yield return new WaitForSeconds(1f);
        gameUI.KekkaPanel.SetActive(false);
        yield return new WaitForSeconds(0.7f);

        StartCoroutine(EnemyAttack());
    }
    //カード合成する
    IEnumerator CardSynthesis()
    {
        gameUI.KekkaPanel.SetActive(true);
        player.Hand.gameObject.SetActive(false);
        cardGuide.SetActive(false);

        Vector2 goal = player.SubmitList[0].transform.position;

        for (int i = 1;i < player.SubmitList.Count; i++)
        {
            StartCoroutine(CardSlide(player.SubmitList[i], goal,0.5f));
        }
        yield return new WaitForSeconds(0.7f);

        //合成カードに変化させほかのカードを壊す
        synthesis.CardSynthesis(player.SubmitList, deck.DeckAll);
        yield return StartCoroutine(CardSlide(player.SubmitList[0], player.SubmitPosition.transform.position, 0.7f));
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(synthesis.Close(player.SubmitList[0]));
        yield return new WaitForSeconds(1f);

        gameUI.KekkaPanel.SetActive(false);
        yield return new WaitForSeconds(0.7f);

        StartCoroutine(EnemyAttack());
    }

    //カードをスライドさせる
    IEnumerator CardSlide(Card card, Vector2 goal, float slideDuration)
    {
        float elapsedTime = 0.0f;

        Vector2 start = card.transform.position;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;

            card.transform.position = Vector2.Lerp(start, goal, elapsedTime / slideDuration);
            yield return null;
        }
        card.transform.position = goal;
        yield break;
    }

    IEnumerator EnemyAttack()
    {
        //敵の攻撃宣言
        ruleBook.TextSetupNext();
        gameUI.KekkaPanel.SetActive(true);
        yield return StartCoroutine(gameUI.Sengen(enemy));

        //敵の攻撃
        ruleBook.EnemyAttack(player, enemy);
        if (player.Life <= 0)
        {
            player.Life = 0;
            gameUI.ShowLifes(player.Life);
            yield return new WaitForSeconds(2f);
            ShowResult();
            yield break;
        }
        gameUI.ShowLifes(player.Life);
        yield return new WaitForSeconds(1.5f);

        gameUI.KekkaPanel.SetActive(false);
        SetupNextTurn();
    }

    //ゲームの結果を表示する
    void ShowResult()
    {
        if (player.Life == 0)
            gameUI.ShowGameResult("LOSE" ,TurnCount);

        else if (enemy.Base.EnemyLife == 0)
            gameUI.ShowGameResult("WIN" ,TurnCount);
    }

    //次ターンに向けてのリセットと準備

    void SetupNextTurn()
    {
        Debug.Log($"敵のLife：{enemy.Base.EnemyLife}");
        player.SetupNext();
        ruleBook.TextSetupNext();
        ruleBook.EnemyCountDown(enemy);

        ResetButton();
        deck.DeckListOpen();
        player.Hand.gameObject.SetActive(true);
        cardGuide.SetActive(true);
        SendCardTo(player);
        
        gameUI.ShowTurn(TurnCount += 1);
    }

    //ボタンを初期化する
    void ResetButton()
    {
        Reaction riaction1 = submitButton.GetComponent<Reaction>();
        Reaction riaction2 = SynthesisButton.GetComponent<Reaction>();
        riaction1.ButtonReSet();
        riaction2.ButtonReSet();
        submitButton.SetActive(true);
        SynthesisButton.SetActive(true);
        synthesis.OnSynthesisPanel();
    }

}
