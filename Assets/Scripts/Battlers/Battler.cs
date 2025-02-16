using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;

    [Header("�v���C���[�x�[�X�X�e�[�^�X")]
    [SerializeField] int lifeMax;
    [SerializeField] int AttackBase;
    [SerializeField] int MagicAttackBase;
    [SerializeField] int HealBase;
    [SerializeField] int guardBase;
    [Space(10)]
    [Header("�����l")]
    [SerializeField] int life;
    [SerializeField] int attack;
    [SerializeField] int magicAttack;
    [SerializeField] int defens;
    [SerializeField] int heal;
    [SerializeField] int guard;

    public UnityAction OnSubmitAction;
    public UnityAction OnSynthesisAction;
    public bool IsSubmitted { get; private set; }
    public int LifeMax { get => lifeMax; set => lifeMax = value; }
    public int Heal { get => heal; set => heal = value; }
    public int Guard { get => guard; set => guard = value; }

    public int Attack { get => attack; set => attack = value; }
    public int MagicAttack { get => magicAttack; set => magicAttack = value; }
    public int Defens { get => defens; set => defens = value; }
    public int Life { get => life; set => life = value; }

    public BattlerHand Hand { get => hand; }
    public SubmitPosition SubmitPosition { get => submitPosition; }
    public Card SubmitCard { get => submitPosition.SubmitCard;} 
    public List<Card> SubmitList { get => submitPosition.Submitlist; }


    public void SetPlayer()
    {
        life = lifeMax;
        SetStatus();
    }

    public void SetStatus()
    {
        Attack = AttackBase;
        MagicAttack = MagicAttackBase;
        Heal = HealBase;
        Guard = guardBase;
    }
    //�������ꂽ�J�[�h�����X�g�ɒǉ��E�J�[�h�N���b�N���̌��ʒǉ�
    public void SerCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    //�J�[�h�N���b�N���̃��A�N�V����
    public void SelectedCard(Card card)
    {
        if (IsSubmitted)
            return;

        if (card.transform.parent == submitPosition.transform)
        {
            submitPosition.ReRemove(card);
            submitPosition.SubmitCard = null;
            hand.Add(card);
            hand.RePosition(card);
            submitPosition.SubmitPositionIn();
            card.PosReset();
            submitPosition.effectReSet(card);
        }
        else if (submitPosition.SubmitCard != null)
        {
            return;
        }
        else if (card.transform.parent == hand.transform)
        {
            submitPosition.Set(card);
            hand.RemoveList(card);
            hand.ResetPosition();
            card.PosReset();
        }
    }

    //����{�^�����͎��ɍs���A�N�V����
    public void OnSubmitButton()
    {
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
    }

    public void OnSynthesisButton()
    {
        IsSubmitted = true;
        OnSynthesisAction?.Invoke();
    }


    //���̃^�[���ł̊֐��̃��Z�b�g
    public void SetupNext()
    {
        IsSubmitted = false;
        submitPosition.DeleteCard();
        Defens = 0;
    }
}
