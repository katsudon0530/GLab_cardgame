using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class RuleBook : MonoBehaviour
{
    [SerializeField] Text kekka;
    [SerializeField] float pluseffect;



    //�ꖇ�O�̃J�[�h�̒ǉ����ʏ���
    public void FlontEffect(Battler player, Card flontCard)
    {
        if (flontCard == null)
        {
            return;
        }
        else
        {
            player.Attack = (int)(player.Attack * flontCard.Base.CardEffect.Attack_Effect);
            player.MagicAttack = (int)(player.MagicAttack * flontCard.Base.CardEffect.Magic_Effect);
            player.Guard = (int)(player.Guard * flontCard.Base.CardEffect.Protection_Effect);
            player.Heal = (int)(player.Heal * flontCard.Base.CardEffect.Heal_Effect);
        }
    }

    //�����J�[�h�Ɋ|����{��
    public void TypeEffect(Battler player, Card card)
    {
        if (card.Base.SynthesisType == SynthesisType.Normal)
        {
        }
        else if (card.Base.SynthesisType == SynthesisType.Plus)
        {
            player.Attack = (int)(player.Attack * pluseffect);
            player.MagicAttack = (int)(player.MagicAttack * pluseffect);
            player.Guard = (int)(player.Guard * pluseffect);
            player.Heal = (int)(player.Heal * pluseffect);
        }
    }

    //�J�[�h�̌��ʏ���
    public void selectedCardVS(Battler player, Card card, Enemy enemy)
    {
        //card.Base.Type.PlayCard(player, enemy, kekka);

        if (card.Base.Type == CardType.Sword)
        {
            int Hit = (int)(player.Attack * Random.Range(0.8f, 1.2f));
            float defense = 1f - enemy.Base.EnemyDefense / 100f;
            int damage = (int)(Hit * defense);
            enemy.Base.EnemyLife -= damage;
            kekka.text = $"{damage}�_���[�W�^����";
            if (enemy.Base.EnemyLife < 0)
            {
                enemy.Base.EnemyLife = 0;
            }
        }
        else if (card.Base.Type == CardType.Witchcraft)
        {

            int Hit = (int)(player.MagicAttack * Random.Range(0.8f, 1.2f));
            float defense = 1f - enemy.Base.EnemyMagicDefense / 100f;
            int damage = (int)(Hit * defense);
            enemy.Base.EnemyLife -= damage;
            kekka.text = $"{damage}���@�_���[�W��������";
            if (enemy.Base.EnemyLife < 0)
            {
                enemy.Base.EnemyLife = 0;
            }
        }
        else if (card.Base.Type == CardType.Protection)
        {

            player.Defens += player.Guard;
            if(player.Defens > 100)
            {
                player.Defens = 100;
            }
            kekka.text = $"{player.Defens}�ڂ����傪��������";
        }
        else if (card.Base.Type == CardType.Heal)
        {

            if ((player.Life + player.Heal) > player.LifeMax)
            {
                player.Heal = player.LifeMax - player.Life;
            }
            player.Life += player.Heal;
            kekka.text = $"{player.Heal}HP�����ӂ�����";
        }

    }

    //�G�l�~�[�̋��͍U���܂ł̃J�E���g�_�E��
    public void EnemyCountDown(Enemy enemy)
    {
        if (enemy.Base.Count1 == 0)
        {
            enemy.Base.Count1 = enemy.Base.EnemyCount;
            enemy.CountText1.text = $"{enemy.Base.Count1}";
        }
        else
        {
            enemy.Base.Count1 = enemy.Base.Count1 - 1;
            enemy.CountText1.text = $"{enemy.Base.Count1}";
        }
    }

    //�G�̃^�[������
    public void EnemyAttack(Battler player, Enemy enemy)
    {
        int Hit = (int)(enemy.Base.EnemyAttack * Random.Range(0.8f, 1.1f));
        float Decrease = 1f - player.Defens / 100f;

        if (enemy.Base.Count1 == 0)
        {
            Hit = 2 * Hit;
        }
        Hit = (int)(Hit * Decrease);
        player.Life -= Hit;
        kekka.text = $"{Hit}�_���[�W��������";

    }

    //�G�̏�ԕ\��
    public void EnemyParLife(Enemy enemy)
    {
        float RestLife = (float)enemy.Base.EnemyLife / (float)enemy.Base.EnemyLifeMax;

        if (RestLife == 1f)
        {
            kekka.text = "�S�������Ă��Ȃ��I";
        }
        else if (RestLife > 0.7f)
        {
            kekka.text = $"{enemy.Base.Name1}�̓s���s�����Ă���";
        }
        else if (RestLife > 0.4f)
        {
            kekka.text = $"{enemy.Base.Name1}�͔��n�߂Ă���";
        }
        else
        {
            kekka.text = $"{enemy.Base.Name1}�͂����{���{�����I";
        }
    }

    //���ʂ̃e�L�X�g���Z�b�g
    public void TextSetupNext()
    {
        kekka.text = "";
    }
}
