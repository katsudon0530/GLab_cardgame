using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Reaction reaction;
    [SerializeField] Text playerLifeText;
    [SerializeField] Text TurnText;
    [SerializeField] Text TurnResultText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject deckPanel;
    [SerializeField] GameObject rulePanel;
    [SerializeField] Text resultText;
    [SerializeField] Text ruleText1;
    [SerializeField] Text ruleText2;
    [SerializeField] Text ruleText3;
    [SerializeField] Text ruleText4;
    [SerializeField] Text ruleText5;
    [SerializeField] Text ruleText6;
    [SerializeField] GameObject susumuButton;
    [SerializeField] GameObject modoruButton;
    [SerializeField] GameObject kekkaPanel;
    [SerializeField] Text kekka;

    public GameObject RulePanel { get => rulePanel; set => rulePanel = value; }
    public GameObject DeckPanel { get => deckPanel; set => deckPanel = value; }
    public GameObject KekkaPanel { get => kekkaPanel; set => kekkaPanel = value; }

    //UI�̔�\����
    public void UISetUp()
    {
        kekkaPanel.SetActive(false);
        resultPanel.SetActive(false);
        rulePanel.SetActive(false);
        SetRuleText();
        deckPanel.SetActive(false);
    }

    public void SetRuleText()
    {
        modoruButton.SetActive(false);
        ruleText1.gameObject.SetActive(true);
        ruleText2.gameObject.SetActive(false);
        ruleText3.gameObject.SetActive(false);
        ruleText4.gameObject.SetActive(false);
        ruleText5.gameObject.SetActive(false);
        ruleText6.gameObject.SetActive(false);
    }

    //�����ǉ���UI��\�����邩���Ȃ����̔���

    //���C�t�̕\���E�ύX
    public void ShowLifes(int playerLife)
    {
        playerLifeText.text = $"{playerLife}HP";
    }

    //�o�߃^�[�������J�E���g����
    public void ShowTurn(int turnCount)
    {
        TurnText.text = $"�^�[�� {turnCount}";
    }

    //�Q�[���̏��s���p�l���ŕ\��
    public void ShowGameResult(string result , int turnCount)
    {
        resultPanel.SetActive(true);
        resultText.text = result;
        if (result == "WIN")
        {
            TurnResultText.gameObject.SetActive(true);
            TurnResultText.text = $"�o�߃^�[���F{turnCount}";
        }
        else
        {
            TurnResultText.gameObject.SetActive(false);
        }
    }

    public void Proceed()
    {
        if (ruleText1.gameObject.activeSelf)
        {
            ruleText1.gameObject.SetActive(false);
            ruleText2.gameObject.SetActive(true);
            modoruButton.SetActive(true);
        }
        else if (ruleText2.gameObject.activeSelf)
        {
            ruleText2.gameObject.SetActive(false);
            ruleText3.gameObject.SetActive(true);
        }
        else if (ruleText3.gameObject.activeSelf)
        {
            ruleText3.gameObject.SetActive(false);
            ruleText4.gameObject.SetActive(true);
        }
        else if (ruleText4.gameObject.activeSelf)
        {
            ruleText4.gameObject.SetActive(false);
            ruleText5.gameObject.SetActive(true);
        }
        else if (ruleText5.gameObject.activeSelf)
        {
            ruleText5.gameObject.SetActive(false);
            ruleText6.gameObject.SetActive(true);
            susumuButton.SetActive(false);
            Reaction riaction = susumuButton.GetComponent<Reaction>();
            riaction.ButtonReSet();
        }
    }

    public void ReturnAA()
    {
        if (ruleText2.gameObject.activeSelf)
        {
            ruleText2.gameObject.SetActive(false);
            ruleText1.gameObject.SetActive(true);
            modoruButton.SetActive(false);
            Reaction riaction = modoruButton.GetComponent<Reaction>();
            riaction.ButtonReSet();
        }
        else if (ruleText3.gameObject.activeSelf)
        {
            ruleText3.gameObject.SetActive(false);
            ruleText2.gameObject.SetActive(true);
        }
        else if (ruleText4.gameObject.activeSelf)
        {
            ruleText4.gameObject.SetActive(false);
            ruleText3.gameObject.SetActive(true);
        }
        else if (ruleText5.gameObject.activeSelf)
        {
            ruleText5.gameObject.SetActive(false);
            ruleText4.gameObject.SetActive(true);
        }
        else if (ruleText6.gameObject.activeSelf)
        {
            ruleText6.gameObject.SetActive(false);
            ruleText5.gameObject.SetActive(true);
            susumuButton.SetActive(true);
        }

    }


    public IEnumerator Sengen(Enemy enemy)
    {
        switch (enemy.Base.Type)
        {
            case EnemyType.Slime:
                kekka.text = $"{enemy.Base.Name1}�̍U��";
                yield return new WaitForSeconds(1f);
                kekka.text = ($"{enemy.Base.Name1}�̗n���t�I");
                yield return new WaitForSeconds(1.2f);
                break;
            case EnemyType.Golem:
                kekka.text = $"{enemy.Base.Name1}�̍U��";
                yield return new WaitForSeconds(1f);
                kekka.text = ($"{enemy.Base.Name1}�̃O���[�g�p���`�I");
                yield return new WaitForSeconds(1.2f);
                break;
            case EnemyType.Dragon:
                kekka.text = ($"{enemy.Base.Name1}�̍U��");
                yield return new WaitForSeconds(1f);
                kekka.text = ($"{enemy.Base.Name1}�̃t�@�C�A�u���X�I");
                yield return new WaitForSeconds(1.2f);
                break;
        }
    }
}
