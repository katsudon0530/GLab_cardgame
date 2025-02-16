using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class CardTypeExtensions
{
    public static void PlayCard(this CardType card, Battler player, Enemy enemy, Text kekka)
    {
        switch (card)
        {
            case CardType.Sword:
                {
                    int Hit = (int)(player.Attack * Random.Range(0.8f, 1.2f));
                    float defense = 1f - enemy.Base.EnemyDefense / 100f;
                    int damage = (int)(Hit * defense);
                    enemy.Base.EnemyLife -= damage;
                    kekka.text = $"{damage}ダメージ与えた";
                    if (enemy.Base.EnemyLife < 0)
                    {
                        enemy.Base.EnemyLife = 0;
                    }
                }
                break;
            case CardType.Witchcraft:
                {
                    int Hit = (int)(player.MagicAttack * Random.Range(0.8f, 1.2f));
                    float defense = 1f - enemy.Base.EnemyMagicDefense / 100f;
                    int damage = (int)(Hit * defense);
                    enemy.Base.EnemyLife -= damage;
                    kekka.text = $"{damage}魔法ダメージあたえた";
                    if (enemy.Base.EnemyLife < 0)
                    {
                        enemy.Base.EnemyLife = 0;
                    }
                }
                break;
            case CardType.Protection:
                {
                    player.Defens += player.Guard;
                    kekka.text = $"{player.Defens}ぼうぎょがあがった";
                }
                break;
            case CardType.Heal:
                {

                    if ((player.Life + player.Heal) > player.LifeMax)
                    {
                        player.Heal = player.LifeMax - player.Life;
                    }
                    player.Life += player.Heal;
                    kekka.text = $"{player.Heal}HPかいふくした";
                }
                break;
            case CardType.Reset:
                {

                }
                break;
        }
    }

}
